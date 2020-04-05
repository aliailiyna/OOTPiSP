using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibraryAttr;
using System.Reflection;

namespace Lab_2
{
    public partial class FormObject : Form
    {
        private object Obj;
        private FormObjectToDo formObjectToDo;
        private int startNumberOfControls;
        private Type currentClass;
        private string strHelp;
        private int deepLevel = 0;
        public FormObject(Object argObject, Type argCurrentClass, FormObjectToDo argFormObjectToDo)
        {
            InitializeComponent();
            Obj = argObject;
            currentClass = argCurrentClass;
            formObjectToDo = argFormObjectToDo;
            startNumberOfControls = Controls.Count;
        }

        private void FormObject_Load(object sender, EventArgs e)
        {
            int topOffset = 50;
            FormCreateFields(currentClass, ref topOffset, 25, 0);
            Height = topOffset + 125;
            Width = Width + 25 * deepLevel;
            FormBlockFields();
            FormSetText(((NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute))).Name);
            Shown += FormObject_Shown;
            FormClosing += FormObject_FormClosing;
        }

        private void FormObject_FormClosing(Object sender, FormClosingEventArgs e)
        {
            switch (DialogResult)
            {
                case DialogResult.OK:
                    switch (formObjectToDo)
                    {
                        case FormObjectToDo.Read:
                            break;
                        //case FormObjectToDo.Create:
                        //case FormObjectToDo.Update:
                        default:
                            int number = startNumberOfControls;
                            SetFieldsToObject(Obj, currentClass, ref number);
                            break;
                    }
                    break;
                default:
                    switch (formObjectToDo)
                    {
                        case FormObjectToDo.Create:
                            break;
                        case FormObjectToDo.Read:
                            break;
                        //case FormObjectToDo.Update:
                        default:
                            break;
                    }
                    break;
            }
        }

        private void FormObject_Shown(Object sender, EventArgs e)
        {
            int number = startNumberOfControls;
            FormSetValues(Obj, ref number);
            CenterToScreen();
        }

        private String GetDescription(Type type, String enumName)
        {
            MemberInfo[] memInfo = type.GetMember(enumName);
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(NameAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((NameAttribute)attrs[0]).Name;
            }
            return enumName;
        }

        private void FormCreateFields(Type currentClass, ref int topOffset, int leftOffsetFirst, int argDeepLevel)
        {
            FieldInfo[] fields = currentClass.GetFields();
            Array.Reverse(fields);
            const int topDifference = 25;
            int leftOffsetSecond = leftOffsetFirst + 150;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsPrimitive)
                {
                    if ((field.FieldType == typeof(ushort)) || (field.FieldType == typeof(byte)))
                    {
                        Controls.Add(new Label() { Left = leftOffsetFirst, Top = topOffset, Name = "lb" + field.Name, Text = ((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute))).Name, AutoSize = true, MaximumSize = new Size() { Width = 150 } });
                        Controls.Add(new NumericUpDown() { Left = leftOffsetSecond, Top = topOffset, Name = "tb" + field.Name, Text = field.Name, Minimum = ushort.MinValue, Maximum = ushort.MaxValue, Value = ushort.MaxValue });
                        topOffset += topDifference;
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        Controls.Add(new CheckBox() { Left = leftOffsetSecond, Top = topOffset, Name = "tb" + field.Name, Text = ((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute))).Name, AutoSize = true });
                        topOffset += topDifference;
                    }
                }
                else if (field.FieldType == typeof(string))
                {
                    Controls.Add(new Label() { Left = leftOffsetFirst, Top = topOffset, Name = "lb" + field.Name, Text = ((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute))).Name, AutoSize = true, MaximumSize = new Size() { Width = 150 } });
                    Controls.Add(new TextBox() { Left = leftOffsetSecond, Top = topOffset, Name = "tb" + field.Name, Text = ((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute))).Name, Width = 150, MaxLength = 20 });
                    topOffset += topDifference;
                }
                else if (field.FieldType.IsEnum)
                {
                    Controls.Add(new Label() { Left = leftOffsetFirst, Top = topOffset, Name = "lb" + field.Name, Text = ((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute))).Name, AutoSize = true, MaximumSize = new Size() { Width = 150 } });
                    ComboBox newComboBox = new ComboBox() { Left = leftOffsetSecond, Top = topOffset, Name = "cb" + field.Name, Text = ((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute))).Name };
                    foreach (String enumName in field.FieldType.GetEnumNames())
                    {
                        String name = GetDescription(field.FieldType, enumName);
                        newComboBox.Items.Add(name);
                    }

                    newComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    Controls.Add(newComboBox);
                    topOffset += topDifference;
                }
                else if (field.FieldType.IsClass)
                {
                    Controls.Add(new Label() { Left = leftOffsetFirst, Top = topOffset, Name = "lb" + field.Name, Text = ((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute))).Name, AutoSize = true, MaximumSize = new Size() { Width = 150 } });
                    topOffset += topDifference;
                    FormCreateFields(field.FieldType, ref topOffset, leftOffsetFirst + 25, argDeepLevel + 1);
                }
                if (argDeepLevel > deepLevel)
                {
                    deepLevel = argDeepLevel;
                }
            }
        }

        public void FormSetValues(Object currentObject, ref int indexControl)
        {
            FieldInfo[] fields = currentObject.GetType().GetFields();
            Array.Reverse(fields);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsPrimitive)
                {
                    if (field.FieldType == typeof(ushort))
                    {
                        indexControl++;
                        ushort value = (ushort)field.GetValue(currentObject);
                        ((NumericUpDown)Controls[indexControl]).Value = value;
                        indexControl++;
                    }
                    else if (field.FieldType == typeof(byte))
                    {
                        indexControl++;
                        byte value = (byte)field.GetValue(currentObject);
                        ((NumericUpDown)Controls[indexControl]).Value = value;
                        indexControl++;
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        bool value = (bool)field.GetValue(currentObject);
                        if (value)
                        {
                            ((CheckBox)Controls[indexControl]).Checked = true;
                        }
                        indexControl++;
                    }
                }
                else if (field.FieldType == typeof(string))
                {
                    indexControl++;
                    string value = (string)field.GetValue(currentObject);
                    ((TextBox)Controls[indexControl]).Text = value;
                    indexControl++;
                }
                else if (field.FieldType.IsEnum)
                {
                    indexControl++;
                    Object value = field.GetValue(currentObject);
                    ((ComboBox)Controls[indexControl]).SelectedItem = GetDescription(field.FieldType, value.ToString());
                    indexControl++;
                }
                else if (field.FieldType.IsClass)
                {
                    indexControl++;
                    Object obj = field.GetValue(currentObject);
                    FormSetValues(obj, ref indexControl);
                }
            }
        }

        public void SetFieldsToObject(Object currentObject, Type currentClass, ref int indexControl)
        {
            FieldInfo[] fields = currentClass.GetFields();
            Array.Reverse(fields);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsPrimitive)
                {
                    if (field.FieldType == typeof(ushort))
                    {
                        indexControl++;
                        ushort value = (ushort)((NumericUpDown)Controls[indexControl]).Value;
                        field.SetValue(currentObject, value);
                        indexControl++;
                    }
                    if (field.FieldType == typeof(byte))
                    {
                        indexControl++;
                        byte value = (byte)((NumericUpDown)Controls[indexControl]).Value;
                        field.SetValue(currentObject, value);
                        indexControl++;
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        bool value = false;
                        if (((CheckBox)Controls[indexControl]).Checked)
                        {
                            value = true;
                        }
                        field.SetValue(currentObject, value);
                        indexControl++;
                    }
                }
                else if (field.FieldType == typeof(string))
                {
                    indexControl++;
                    var value = ((TextBox)Controls[indexControl]).Text;
                    field.SetValue(currentObject, value);
                    indexControl++;
                }
                else if (field.FieldType.IsEnum)
                {
                    indexControl++;
                    String currentName = ((ComboBox)Controls[indexControl]).SelectedItem.ToString();
                    foreach (var enumItem in field.FieldType.GetEnumValues())
                    {
                        var enumName = enumItem.ToString();
                        var name = GetDescription(field.FieldType, enumName);
                        if (name == currentName)
                        {
                            field.SetValue(currentObject, enumItem);
                            break;
                        }
                    }
                    indexControl++;
                }
                else if (field.FieldType.IsClass)
                {
                    indexControl++;
                    Object obj = field.GetValue(currentObject);
                    SetFieldsToObject(obj, field.FieldType, ref indexControl);
                    field.SetValue(currentObject, obj);
                }
            }
        }

        private void FormBlockFields()
        {
            if (formObjectToDo == FormObjectToDo.Read)
            {
                btCancel.Enabled = false;
                btCancel.Visible = false;
                btOk.DialogResult = DialogResult.Cancel;
                btOk.Left = btOk.Left + 90;
                for (int i = startNumberOfControls; i < Controls.Count; i++)
                {
                    Type controlType = Controls[i].GetType();
                    if (controlType == typeof(TextBox))
                    {
                        ((TextBox)Controls[i]).ReadOnly = true;
                    }
                    else if (controlType == typeof(NumericUpDown))
                    {
                        ((NumericUpDown)Controls[i]).ReadOnly = true;
                        ((NumericUpDown)Controls[i]).Increment = 0;
                    }
                    else if (controlType == typeof(CheckBox))
                    {
                        ((CheckBox)Controls[i]).Enabled = false;
                    }
                    else if (controlType == typeof(ComboBox))
                    {
                        ((ComboBox)Controls[i]).Enabled = false;
                    }
                }
            }
        }

        private void miHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(strHelp, "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormSetText(String name)
        {
            switch (formObjectToDo)
            {
                case FormObjectToDo.Create:
                    Text = "Создание объекта";
                    strHelp = "Создание нового объекта. Необходимо ввести свойства создаваемого объекта.";
                    toolStripStatusLabel.Text = "Создание: " + name;
                    break;
                case FormObjectToDo.Read:
                    Text = "Просмотр объекта";
                    strHelp = "Просмотр существующего объекта. Отображены свойства выбранного объекта.";
                    toolStripStatusLabel.Text = "Просмотр: " + name;
                    break;
                //case FormObjectToDo.Update:
                default:
                    Text = "Редактирование объекта";
                    strHelp = "Редактирование существующего объекта. Возможно отредактировать свойства выбранного объекта.";
                    toolStripStatusLabel.Text = "Редактирование: " + name;
                    break;
            }
        }
    }
}
