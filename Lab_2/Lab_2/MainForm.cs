using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using ClassLibrary;
using ClassLibraryAttr;
using System.IO;

namespace Lab_2
{
    public partial class MainForm : Form
    {
        const int MAX_NUMBER_OF_OBJECTS = 10;
        private Dictionary<Type, Dictionary<string, object>> furniture = new Dictionary<Type, Dictionary<string, object>>();
        private Assembly classesAssembly;
        private List<Type> classesTypesLibrary = new List<Type>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void SetComboBoxOnFirst(ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                cmb.SelectedIndex = 0;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //@"G:\_4_semestr\3.OOTPiSP\Lab_2\Lab_2_2\Lab_2\ClassLibrary\bin\Debug\netstandard2.0\ClassLibrary.dll"
            string path = Path.GetFullPath("../../../ClassLibrary/bin/Release/netstandard2.0/ClassLibrary.dll");
            classesAssembly = Assembly.LoadFile(path);
            classesTypesLibrary = classesAssembly.GetTypes().Where(type => type.IsClass).ToList();
            foreach (Type currentClass in classesTypesLibrary)
            {
                if (!currentClass.IsAbstract)
                {
                    NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                    if (attr != null)
                    {
                        cmbClasses.Items.Add(attr.Name);
                    }
                    furniture.Add(currentClass, new Dictionary<string, object>());
                }
            }
            SetComboBoxOnFirst(cmbClasses);
        }

        private void cmbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbObjects.Items.Clear();
            String currentAttr = cmbClasses.SelectedItem.ToString();
            Type currentClass = typeof(object);
            if (FindClassTypeByNameAttr(currentAttr, ref currentClass))
            {
                foreach (var currentObject in furniture[currentClass])
                {
                    cmbObjects.Items.Add(currentObject.Key);
                }
            }
            SetComboBoxOnFirst(cmbObjects);
        }

        private Object FindObjectByName(Type currentClass, String name)
        {
            foreach (var currentObject in furniture[currentClass])
            {
                if (currentObject.Key == name)
                {
                    return currentObject.Value;
                }
            }
            return null;
        }

        private void CreateAddForm(Type currentClass)
        {
            Object currentObject = classesAssembly.CreateInstance(currentClass.FullName);
            bool toContinue = false;
            do
            {
                FormObject addForm = new FormObject(currentObject, currentClass, FormObjectToDo.Create);
                switch (addForm.ShowDialog())
                {
                    case DialogResult.OK:
                        FieldInfo field = currentObject.GetType().GetField("Name");
                        String name = (String)field.GetValue(currentObject);
                        if (FindObjectByName(currentClass, name) == null)
                        {
                            if (name == "")
                            {
                                toContinue = true;
                                MessageBox.Show("Именя объекта не может быть пустой строкой.",
                                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                toContinue = false;
                                furniture[currentClass].Add(name, currentObject);
                                cmbObjects.Items.Add(name);
                                cmbObjects.SelectedItem = name;
                            }
                        }
                        else
                        {
                            toContinue = true;
                            MessageBox.Show("Имена объектов заданного типа не должны повторяться.", 
                                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    default:
                        toContinue = false;
                        break;
                }
            } 
            while (toContinue);
        }

        private void CreateReadForm(Type currentClass, Object currentObject)
        {
            FormObject readForm = new FormObject(currentObject, currentClass, FormObjectToDo.Read);
            readForm.ShowDialog();
        }

        private Object GetCopyOfObject(Type currentClass, Object sourseObject)
        {
            Object destObject = classesAssembly.CreateInstance(currentClass.FullName);
            FieldInfo[] fields = currentClass.GetFields();
            foreach (FieldInfo field in fields)
            {
                if ((field.FieldType.IsPrimitive) || (field.FieldType.IsEnum) || (field.FieldType == typeof(string)))
                {
                    field.SetValue(destObject, field.GetValue(sourseObject));
                }
                else if (field.FieldType.IsClass)
                {
                    Object objectOld = field.GetValue(sourseObject);
                    field.SetValue(destObject, GetCopyOfObject(field.FieldType, objectOld));
                }
                
            }
            return destObject;
        }

        private void CreateUpdateForm(Type currentClass, Object currentObject)
        {
            Object copyObject = GetCopyOfObject(currentClass, currentObject);
            bool toContinue = false;
            do
            {
                FormObject updateForm = new FormObject(copyObject, currentClass, FormObjectToDo.Update);
                switch (updateForm.ShowDialog())
                {
                    case DialogResult.OK:
                        FieldInfo fieldNew = copyObject.GetType().GetField("Name");
                        String nameNew = (String)fieldNew.GetValue(copyObject);

                        if (nameNew == "")
                        {
                            toContinue = true;
                            MessageBox.Show("Именя объекта не может быть пустой строкой.",
                                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            FieldInfo fieldOld = currentObject.GetType().GetField("Name");
                            String nameOld = (String)fieldOld.GetValue(currentObject);

                            if ((nameNew == nameOld) || (FindObjectByName(currentClass, nameNew) == null))
                            {
                                toContinue = false;

                                furniture[currentClass].Remove(nameOld);
                                cmbObjects.Items.Remove(nameOld);

                                furniture[currentClass].Add(nameNew, copyObject);
                                cmbObjects.Items.Add(nameNew);

                                cmbObjects.SelectedItem = nameNew;
                            }
                            else
                            {
                                toContinue = true;
                                MessageBox.Show("Имена объектов заданного типа не должны повторяться.",
                                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        break;
                    default:
                        toContinue = false;
                        break;
                }
            }
            while (toContinue);
        }

        private bool FindClassTypeByNameAttr(String currentAttr, ref Type currentType)
        {
            foreach (Type currentClass in classesTypesLibrary)
            {
                NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                if ((attr != null) && (currentAttr == attr.Name))
                {
                    currentType = currentClass;
                    return true;
                }
            }
            return false;
        }

        private void btAddObject_Click(object sender, EventArgs e)
        {
            String currentAttr = cmbClasses.SelectedItem.ToString();
            Type currentClass = typeof(object);
            if (FindClassTypeByNameAttr(currentAttr, ref currentClass))
            {
                if (furniture[currentClass].Count < MAX_NUMBER_OF_OBJECTS)
                {
                    CreateAddForm(currentClass);
                }
                else
                {
                    MessageBox.Show("Число объектов одного типа не должно превышать " + 
                        MAX_NUMBER_OF_OBJECTS.ToString() + ".", "Предупреждение", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btReadObject_Click(object sender, EventArgs e)
        {
            if (cmbObjects.Items.Count > 0)
            {
                String currentAttrClasses = cmbClasses.SelectedItem.ToString();
                Type currentClass = typeof(object);
                if (FindClassTypeByNameAttr(currentAttrClasses, ref currentClass))
                {
                    String currentObjectName = cmbObjects.SelectedItem.ToString();
                    Object currentObject = FindObjectByName(currentClass, currentObjectName);
                    if (currentObject != null)
                    {
                        CreateReadForm(currentClass, currentObject);
                    }
                }
            }
            else
            {
                MessageBox.Show("Список объектов выбранного типа пуст.", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btUpdateObject_Click(object sender, EventArgs e)
        {
            if (cmbObjects.Items.Count > 0)
            {
                String currentAttr = cmbClasses.SelectedItem.ToString();
                Type currentClass = typeof(object);
                if (FindClassTypeByNameAttr(currentAttr, ref currentClass))
                {
                    String currentObjectName = cmbObjects.SelectedItem.ToString();
                    Object currentObject = FindObjectByName(currentClass, currentObjectName);
                    if (currentObject != null)
                    {
                        CreateUpdateForm(currentClass, currentObject);
                    }
                }
            }
            else
            {
                MessageBox.Show("Список объектов выбранного типа пуст.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btDeleteObject_Click(object sender, EventArgs e)
        {
            if (cmbObjects.Items.Count > 0)
            {
                String currentAttr = cmbClasses.SelectedItem.ToString();
                Type currentClass = typeof(object);
                if (FindClassTypeByNameAttr(currentAttr, ref currentClass))
                {
                    String currentObjectName = cmbObjects.SelectedItem.ToString();
                    Object currentObject = FindObjectByName(currentClass, currentObjectName);
                    if (currentObject != null)
                    {
                        switch (MessageBox.Show("Выбранный объект будет удален и информация о нем будет потеряна. " +
                            "Вы уверены, что хотите продолжить?", "Предупреждение",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                        {
                            case DialogResult.Yes:
                                furniture[currentClass].Remove(currentObjectName);
                                cmbObjects.Items.Remove(currentObjectName);
                                SetComboBoxOnFirst(cmbObjects);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Список объектов выбранного типа пуст.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void miAboutProgram_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа представляет собой простой CRUD (Create, Read, Update, Delete) " +
                "редактор объектов с хранением данных в оперативной памяти. Возможно добавление не более " +
                MAX_NUMBER_OF_OBJECTS.ToString() + " объектов одного типа.", 
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miAboutDeveloper_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа была разработана Ильиной Александрой, \n" +
                "студенткой группы 851001", "О разработчике", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miTask_Click(object sender, EventArgs e)
        {
            MessageBox.Show("           Задание к лабораторной работе 2 \"Редактор объектов\"\n\n" +
                "           Необходимо описать разработанную в первой работе иерархию классов на языке программирования и " +
                "реализовать программу с графическим интерфейсом для редактирования свойств объектов.\n\n" +
                "           Пользовательский интерфейс должен содержать список объектов, доступных для редактирования: " +
                "должна быть реализована возможность добавления, удаления и редактирования всех свойств объектов. " +
                "Следует учитывать сценарий редактирования вложенных объектов " +
                "(см.требование о наличии агрегации в иерархии классов).\n\n" +
                "           Получившаяся программа должная представлять собой простой CRUD (Create, Read, Update, Delete) " +
                "редактор объектов выбранной иерархии с хранением данных в оперативной памяти.\n\n" +
                "           Все операции с объектами должны выполняться таким образом, чтобы добавление нового класса в систему " +
                "не требовало изменения существующего кода(выбор типа с помощью оператора switch/case делать нельзя).\n\n",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miAdditionalTask_Click(object sender, EventArgs e)
        {
            MessageBox.Show("           Дополнительное задание к лабораторной работе 2 \"Редактор объектов\"\n\n" +
                "           Реализовать автоматическое создание элементов управления для редактирования свойств объектов " +
                "по структуре редактируемого объекта. Структуру редактируемого объекта следует определять " +
                "с помощью интроспекции (рефлексии).\n\n" +
                "           Для текстовых и числовых свойств должны генерироваться текстовые поля, для свойств типа enum — " +
                "выпадающие списки. Все поля должны быть подписаны: в качестве подписи допускается использовать имя свойства, " +
                "определенное в программе, однако предпочтительно использовать человекочитаемое имя, задаваемое " +
                "в виде метаданных поля (аннотации в Java, атрибуты в C#) или через конфигурационный файл.",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
