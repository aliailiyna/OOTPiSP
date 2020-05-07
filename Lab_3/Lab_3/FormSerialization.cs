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
using ISerializationClassLibrary;
using System.Runtime.InteropServices;
using FurnitureListLibrary;

namespace Lab_3
{
    public partial class FormSerialization : Form
    {
        private List<Type> classesSerializationTypesList;
        private List<NameAttribute> attributesSerializationList;
        FurnitureList objectsDictionary;
        private FormSerializationToDo formSerializationToDo;
        private Assembly classesAssemblySerialization;
        private BooleanRef IsChanged;
        public FormSerialization(List<Type> argClassesSerializationTypesList, List<NameAttribute> argAttributesSerializationList, 
            FurnitureList argObjectsDictionary, FormSerializationToDo argFormSerializationToDo, 
            Assembly argClassesAssemblySerialization, BooleanRef argIsChanged)
        {
            InitializeComponent();
            classesSerializationTypesList = argClassesSerializationTypesList;
            attributesSerializationList = argAttributesSerializationList;
            objectsDictionary = argObjectsDictionary;
            formSerializationToDo = argFormSerializationToDo;
            classesAssemblySerialization = argClassesAssemblySerialization;
            IsChanged = argIsChanged;
        }

        private void cbSerialization_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormSerialization_Load(object sender, EventArgs e)
        {
            foreach(NameAttribute attr in attributesSerializationList)
            {
                cmbSerialization.Items.Add(attr.Name);
            }
            if (cmbSerialization.Items.Count > 0)
            {
                cmbSerialization.SelectedIndex = 0;
            }
            switch (formSerializationToDo)
            {
                case FormSerializationToDo.Open:
                    this.Text = "Открыть файл";
                    this.toolStripStatusLabel.Text = "Выбор типа сериализации для загрузки списка объектов из файла";
                    break;
                case FormSerializationToDo.Save:
                    this.Text = "Сохранить файл";
                    this.toolStripStatusLabel.Text = "Выбор типа сериализации для сохранения списка объектов в файл";
                    break;
            }
            /*foreach (Type currentClass in classesSerializationTypesList)
            {
                if (!currentClass.IsAbstract)
                {
                    NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                    if (attr != null)
                    {
                        cmbSerialization.Items.Add(attr.Name);
                    }
                }
            }*/
        }

        private void btChooseFile_Click(object sender, EventArgs e)
        {
            switch (formSerializationToDo)
            {
                case FormSerializationToDo.Open:
                    if (dlgFileOpen.ShowDialog() == DialogResult.Cancel)
                        return;
                    tbFileName.Text = dlgFileOpen.FileName;
                    break;
                case FormSerializationToDo.Save:
                    if (dlgFileSave.ShowDialog() == DialogResult.Cancel)
                        return;
                    tbFileName.Text = dlgFileSave.FileName;
                    break;
            }
        }

        private bool FindClassTypeByNameAttr(String currentAttr, ref Type currentType)
        {
            foreach (Type currentClass in classesSerializationTypesList)
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

        private int Serialize(ISerialization Serialization)
        {
            int result = Serialization.Serialize(objectsDictionary.GetListFromDictionary(), tbFileName.Text);
            return result;
        }

        private int DeserializePart(List<object> newList)
        {
            if (newList.Count == 0)
            {
                return Constants.EMPTY_LIST;
            }
            else
            {
                objectsDictionary.ClearDictionary();
                objectsDictionary.GetDictionaryFromList(newList);
                return Constants.OK;
            }
        }

        private int Deserialize(ISerialization Serialization)
        {
            List<object> newList = new List<object>();
            int result = Serialization.Deserialize(ref newList, tbFileName.Text);
            if (result == 0)
            {
                if (objectsDictionary.IsDictionaryEmpty())
                {
                    return DeserializePart(newList);
                }
                else
                {
                    switch (MessageBox.Show("Список объектов будет загружен из файла. Текущий список объектов будет утерян. Вы уверены, что хотите продолжить?", "Предупреждение",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                    {
                        case DialogResult.Yes:
                            return DeserializePart(newList);      
                        default:
                            return Constants.USER_CANCEL;
                    }
                }
            }
            else
            {
                return result;
            }
        }

        private void FormSerialization_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (this.DialogResult)
            {
                case DialogResult.OK:
                    //e.Cancel = true;
                    String currentAttr = cmbSerialization.SelectedItem.ToString();
                    Type currentClass = typeof(object);
                    switch (formSerializationToDo)
                    {
                        case FormSerializationToDo.Open:
                            if (FindClassTypeByNameAttr(currentAttr, ref currentClass))
                            {
                                ISerialization Serialization = (ISerialization)classesAssemblySerialization.CreateInstance(currentClass.FullName);
                                switch (Deserialize(Serialization))
                                {
                                    case Constants.OK:
                                        IsChanged.value = true;
                                        MessageBox.Show("Десериализация прошла успешно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case Constants.ERROR_SERIALIZATION:
                                        e.Cancel = true;
                                        MessageBox.Show("Ошибка десериализации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case Constants.ERROR_FILE:
                                        e.Cancel = true;
                                        MessageBox.Show("Выбранного файла не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case Constants.EMPTY_FILE_NAME:
                                        e.Cancel = true;
                                        MessageBox.Show("Имя файла не должно быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    case Constants.USER_CANCEL:
                                        e.Cancel = true;
                                        MessageBox.Show("Отменено пользователем.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case Constants.EMPTY_LIST:
                                        e.Cancel = true;
                                        MessageBox.Show("Файл содержит пустой список объектов. Нельзя загрузить пустой список объектов из файла.", "Предупреждение",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                }
                            }
                            break;
                        case FormSerializationToDo.Save:
                            if (FindClassTypeByNameAttr(currentAttr, ref currentClass))
                            {
                                ISerialization Serialization = (ISerialization)classesAssemblySerialization.CreateInstance(currentClass.FullName);
                                switch (Serialize(Serialization))
                                {
                                    case Constants.OK:
                                        MessageBox.Show("Сериализация прошла успешно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case Constants.ERROR_SERIALIZATION:
                                        e.Cancel = true;
                                        MessageBox.Show("Ошибка сериализации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case Constants.ERROR_FILE:
                                        e.Cancel = true;
                                        MessageBox.Show("Выбранного файла не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case Constants.EMPTY_FILE_NAME:
                                        e.Cancel = true;
                                        MessageBox.Show("Имя файла не должно быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    case Constants.USER_CANCEL:
                                    case Constants.EMPTY_LIST:
                                        break;
                                }
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
