using System;
using System.IO;
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
using ClassLibraryIPlugin;

namespace Lab_4
{
    public partial class FormSerialization : Form
    {
        private List<Type> classesSerializationTypesList;
        private List<NameAttribute> attributesSerializationList;
        FurnitureList objectsDictionary;
        private FormSerializationToDo formSerializationToDo;
        private Assembly classesAssemblySerialization;
        private BooleanRef IsChanged;

        //private readonly string pluginPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        private readonly string pluginPath = @"D:\OOTPiSP\Lab_4\Plugins";
        private readonly string noPlugin = "Нет";
        private readonly string fileNameBuffer = "Buffer";
        private List<Type> pluginsTypesList = new List<Type>();
        private Dictionary<String, IPlugin> pluginsDictionary = new Dictionary<String, IPlugin>();
        private Dictionary<String, String> extensionsDictionary = new Dictionary<String, String>();

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
        private void SetComboBoxOnFirst(ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                cmb.SelectedIndex = 0;
            }
        }

        private void FormSerialization_Load(object sender, EventArgs e)
        {
            foreach(NameAttribute attr in attributesSerializationList)
            {
                cmbSerialization.Items.Add(attr.Name);
            }
            SetComboBoxOnFirst(cmbSerialization);
            switch (formSerializationToDo)
            {
                case FormSerializationToDo.Open:
                    this.Text = "Открыть файл";
                    this.toolStripStatusLabel.Text = "Выбор типа сериализации и типа кодирования для загрузки списка объектов из файла";
                    break;
                case FormSerializationToDo.Save:
                    this.Text = "Сохранить файл";
                    this.toolStripStatusLabel.Text = "Выбор типа сериализации и типа кодирования для сохранения списка объектов в файл";
                    btCheckPlugin.Enabled = false;
                    btCheckPlugin.Visible = false;
                    break;
            }
            RefreshPlugins();
            cmbCoding.Items.Add(noPlugin);
            foreach (Type currentClass in pluginsTypesList)
            {
                NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                if (attr != null)
                {
                    cmbCoding.Items.Add(attr.Name);
                }
            }
            SetComboBoxOnFirst(cmbCoding);
        }

        private void RefreshPlugins()
        {
            pluginsTypesList.Clear();
            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();
            
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);

                var types = asm.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(IPlugin).FullName).Any());

                foreach (var type in types)
                {
                    NameAttribute attr = (NameAttribute)type.GetCustomAttribute(typeof(NameAttribute));
                    if (attr != null)
                    {   
                        pluginsTypesList.Add(type);
                        var plugin = asm.CreateInstance(type.FullName) as IPlugin;
                        pluginsDictionary.Add(attr.Name, plugin);
                    }
                    ExtensionAttribute attrExtension = (ExtensionAttribute)type.GetCustomAttribute(typeof(ExtensionAttribute));
                    extensionsDictionary.Add(attr.Name, attrExtension.Extension);
                }
            }
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

        private bool FindClassTypeByNameAttr(String currentAttr, ref Type currentType, List<Type> classesList)
        {
            if (currentAttr == noPlugin)
            {
                currentType = null;
                return true;
            }
            foreach (Type currentClass in classesList)
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

        private int Serialize(ISerialization Serialization, string currentAttrCoding)
        {
            int result;
            if (currentAttrCoding == noPlugin)
            {
                result = Serialization.Serialize(objectsDictionary.GetListFromDictionary(), tbFileName.Text);
            }
            else
            {
                result = Serialization.Serialize(objectsDictionary.GetListFromDictionary(), fileNameBuffer);
            }
            return result;
        }

        private int DeserializePart(List<object> newList)
        {
            if (newList.Count == 0)
            {
                return ConstantsSerialization.EMPTY_LIST;
            }
            else
            {
                objectsDictionary.ClearDictionary();
                objectsDictionary.GetDictionaryFromList(newList);
                return ConstantsSerialization.OK;
            }
        }

        private int Deserialize(ISerialization Serialization, string currentAttrCoding)
        {
            int result;
            List<object> newList = new List<object>();
            if (currentAttrCoding == noPlugin)
            {
                result = Serialization.Deserialize(ref newList, tbFileName.Text);
            }
            else
            {
                result = Serialization.Deserialize(ref newList, fileNameBuffer);
                try
                {
                    File.Delete(fileNameBuffer);
                }
                catch
                {

                }
            }
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
                            return ConstantsSerialization.USER_CANCEL;
                    }
                }
            }
            else
            {
                return result;
            }
        }

        private int Encode(string currentAttrCoding)
        {
            IPlugin Plugin = pluginsDictionary[currentAttrCoding];
            int result = Plugin.Encode(fileNameBuffer, tbFileName.Text);
            if (Path.GetExtension(tbFileName.Text) != extensionsDictionary[currentAttrCoding])
            {
                try
                {
                    File.Move(tbFileName.Text, tbFileName.Text + extensionsDictionary[currentAttrCoding]);
                }
                catch
                {

                }
            }
            try
            {
                File.Delete(fileNameBuffer);
            }
            catch
            {

            }
            return result;
        }

        private Boolean SerializeAndEncode(string currentAttrSerialization, ref Type currentClassSerialization, string currentAttrCoding, ref Type currentClassCoding)
        {
            Boolean result = false;
            if (FindClassTypeByNameAttr(currentAttrSerialization, ref currentClassSerialization, classesSerializationTypesList) &&
                FindClassTypeByNameAttr(currentAttrCoding, ref currentClassCoding, pluginsTypesList))
            {
                ISerialization Serialization = (ISerialization)classesAssemblySerialization.CreateInstance(currentClassSerialization.FullName);
                switch (Serialize(Serialization, currentAttrCoding))
                {
                    case ConstantsSerialization.OK:
                        if (currentAttrCoding == noPlugin)
                        {
                            result = true;
                            MessageBox.Show("Сериализация прошла успешно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            switch (Encode(currentAttrCoding))
                            {
                                case ConstantsPlugins.OK:
                                    result = true;
                                    MessageBox.Show("Сериализация и кодирование прошли успешно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                case ConstantsPlugins.ERROR:
                                    MessageBox.Show("Ошибка кодирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        break;
                    case ConstantsSerialization.ERROR_SERIALIZATION:
                        MessageBox.Show("Ошибка сериализации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case ConstantsSerialization.ERROR_FILE:
                        MessageBox.Show("Выбранного файла не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case ConstantsSerialization.EMPTY_FILE_NAME:
                        MessageBox.Show("Имя файла не должно быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case ConstantsSerialization.USER_CANCEL:
                        result = true;
                        break;
                    case ConstantsSerialization.EMPTY_LIST:
                        result = true;
                        break;
                }
            }
            return result;
        }

        private int Decode(string currentAttrCoding)
        {
            IPlugin Plugin = pluginsDictionary[currentAttrCoding];
            return Plugin.Decode(tbFileName.Text, fileNameBuffer);
        }

        private Boolean DecodeAndDeserialize(string currentAttrSerialization, ref Type currentClassSerialization, string currentAttrCoding, ref Type currentClassCoding)
        {
            Boolean result = false;
            if (FindClassTypeByNameAttr(currentAttrSerialization, ref currentClassSerialization, classesSerializationTypesList) &&
                FindClassTypeByNameAttr(currentAttrCoding, ref currentClassCoding, pluginsTypesList))
            {
                Boolean toContinue = true;
                if (currentAttrCoding != noPlugin)
                {
                    switch (Decode(currentAttrCoding))
                    {
                        case ConstantsPlugins.OK:
                            break;
                        case ConstantsPlugins.ERROR:
                            MessageBox.Show("Ошибка декодирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            toContinue = false;
                            break;
                    }
                }
                if (toContinue)
                {
                    ISerialization Serialization = (ISerialization)classesAssemblySerialization.CreateInstance(currentClassSerialization.FullName);
                    switch (Deserialize(Serialization, currentAttrCoding))
                    {
                        case ConstantsSerialization.OK:
                            IsChanged.value = true;
                            result = true;
                            if (currentAttrCoding == noPlugin)
                            {
                                MessageBox.Show("Десериализация прошла успешно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Декодирование и десериализация прошли успешно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case ConstantsSerialization.ERROR_SERIALIZATION:
                            MessageBox.Show("Ошибка десериализации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ConstantsSerialization.ERROR_FILE:
                            MessageBox.Show("Выбранного файла не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ConstantsSerialization.EMPTY_FILE_NAME:
                            MessageBox.Show("Имя файла не должно быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case ConstantsSerialization.USER_CANCEL:
                            MessageBox.Show("Отменено пользователем.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case ConstantsSerialization.EMPTY_LIST:
                            MessageBox.Show("Файл содержит пустой список объектов. Нельзя загрузить пустой список объектов из файла.", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                }
            }
            return result;
        }

        private void FormSerialization_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (this.DialogResult)
            {
                case DialogResult.OK:
                    //e.Cancel = true;
                    String currentAttrSerialization = cmbSerialization.SelectedItem.ToString();
                    Type currentClassSerialization = typeof(object);
                    String currentAttrCoding = cmbCoding.SelectedItem.ToString();
                    Type currentClassCoding = typeof(object);
                    switch (formSerializationToDo)
                    {
                        case FormSerializationToDo.Open:
                            if (!DecodeAndDeserialize(currentAttrSerialization, ref currentClassSerialization, currentAttrCoding, ref currentClassCoding))
                            {
                                e.Cancel = true;
                            }
                            break;
                        case FormSerializationToDo.Save:
                            if (!SerializeAndEncode(currentAttrSerialization, ref currentClassSerialization, currentAttrCoding, ref currentClassCoding))
                            {
                                e.Cancel = true;
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private void CheckPlugin()
        {
            if (tbFileName.Text != "")
            {
                String Extension = Path.GetExtension(tbFileName.Text);
                Boolean isFound = false;
                foreach (KeyValuePair<String, String> keyValue in extensionsDictionary)
                {
                    if (Extension == keyValue.Value)
                    {
                        cmbCoding.SelectedItem = keyValue.Key;
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    cmbCoding.SelectedItem = noPlugin;
                }
            }
        }

        private void btCheckPlugin_Click(object sender, EventArgs e)
        {
            CheckPlugin();
        }

        private void tbFileName_TextChanged(object sender, EventArgs e)
        {
            CheckPlugin();
        }
    }
}
