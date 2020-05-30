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
using SerializationClassLibrary;
using FurnitureListLibrary;

namespace Lab_4
{
    public partial class MainForm : Form
    {
        const int MAX_NUMBER_OF_OBJECTS = 10;

        private FurnitureList furniture = new FurnitureList();

        private Assembly classesAssembly;
        private List<Type> classesTypesLibrary = new List<Type>();

        private Assembly classesAssemblySerialization;
        private List<Type> classesSerializationTypesLibrary = new List<Type>();
        private List<NameAttribute> attributesSerialization = new List<NameAttribute>();

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
            classesAssembly = Assembly.LoadFile(@"D:\OOTPiSP\Lab_4\ClassLibrary\bin\Debug\netstandard2.0\ClassLibrary.dll");
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
                    furniture.furniture.Add(currentClass, new Dictionary<string, object>());
                }
            }
            SetComboBoxOnFirst(cmbClasses);

            classesAssemblySerialization = Assembly.LoadFile(@"D:\OOTPiSP\Lab_4\SerializationClassLibrary\bin\Debug\netstandard2.0\SerializationClassLibrary.dll");
            classesSerializationTypesLibrary = classesAssemblySerialization.GetTypes().Where(type => type.IsClass).ToList();
            foreach (Type currentClass in classesSerializationTypesLibrary)
            {
                NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                attributesSerialization.Add(attr);
            }
        }

        private void cmbObjectsUpdate()
        {            
            cmbObjects.Items.Clear();
            String currentAttr = cmbClasses.SelectedItem.ToString();
            Type currentClass = typeof(object);
            if (FindClassTypeByNameAttr(currentAttr, ref currentClass))
            {
                foreach (var currentObject in furniture.furniture[currentClass])
                {
                    cmbObjects.Items.Add(currentObject.Key);
                }
            }
            SetComboBoxOnFirst(cmbObjects);
        }

        private void cmbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbObjectsUpdate();
        }

        private Object FindObjectByName(Type currentClass, String name)
        {
            foreach (var currentObject in furniture.furniture[currentClass])
            {
                if (currentObject.Key == name)
                {
                    return currentObject.Value;
                }
            }
            return null;
        }

        private String GetObjectFullName(String className, String objectName)
        {
            return (className + " " + objectName);
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
                        String currentObjectName = (String)field.GetValue(currentObject);
                        if (FindObjectByName(currentClass, currentObjectName) == null)
                        {
                            if (currentObjectName == "")
                            {
                                toContinue = true;
                                MessageBox.Show("Именя объекта не может быть пустой строкой.",
                                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                toContinue = false;
                                furniture.furniture[currentClass].Add(currentObjectName, currentObject);
                                cmbObjects.Items.Add(currentObjectName);
                                cmbObjects.SelectedItem = currentObjectName;
                                NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                                String fullObjectName = GetObjectFullName(attr.Name, currentObjectName);
                                lbFullObjectsList.Items.Add(fullObjectName);
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
            /*Object destObject = classesAssembly.CreateInstance(currentClass.FullName);
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
            return destObject;*/

            Object destObject = classesAssembly.CreateInstance(currentClass.FullName);
            FieldInfo[] fields = sourseObject.GetType().GetFields();
            FieldInfo[] fieldsDest = destObject.GetType().GetFields();
            for (var i = 0; i < fields.Length; i++)
            {
                if ((fields[i].FieldType.IsPrimitive) || (fields[i].FieldType.IsEnum) || (fields[i].FieldType == typeof(string)))
                {
                    fieldsDest[i].SetValue(destObject, fields[i].GetValue(sourseObject));
                }
                else if (fields[i].FieldType.IsClass)
                {
                    Object objectOld = fields[i].GetValue(sourseObject);
                    fieldsDest[i].SetValue(destObject, GetCopyOfObject(fields[i].FieldType, objectOld));
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
                        String currentObjectNameNew = (String)fieldNew.GetValue(copyObject);

                        if (currentObjectNameNew == "")
                        {
                            toContinue = true;
                            MessageBox.Show("Именя объекта не может быть пустой строкой.",
                                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            FieldInfo fieldOld = currentObject.GetType().GetField("Name");
                            String currentObjectNameOld = (String)fieldOld.GetValue(currentObject);
                            NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                            String fullObjectNameOld = GetObjectFullName(attr.Name, currentObjectNameOld);
                            String fullObjectNameNew = GetObjectFullName(attr.Name, currentObjectNameNew);

                            if ((currentObjectNameNew == currentObjectNameOld) || (FindObjectByName(currentClass, currentObjectNameNew) == null))
                            {
                                toContinue = false;

                                furniture.furniture[currentClass].Remove(currentObjectNameOld);
                                cmbObjects.Items.Remove(currentObjectNameOld);
                                lbFullObjectsList.Items.Remove(fullObjectNameOld);
                                
                                furniture.furniture[currentClass].Add(currentObjectNameNew, copyObject);
                                cmbObjects.Items.Add(currentObjectNameNew);
                                lbFullObjectsList.Items.Add(fullObjectNameNew);

                                cmbObjects.SelectedItem = currentObjectNameNew;
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
                if (furniture.furniture[currentClass].Count < MAX_NUMBER_OF_OBJECTS)
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
                                furniture.furniture[currentClass].Remove(currentObjectName);
                                cmbObjects.Items.Remove(currentObjectName);
                                SetComboBoxOnFirst(cmbObjects);
                                NameAttribute attr = (NameAttribute)currentClass.GetCustomAttribute(typeof(NameAttribute));
                                String fullObjectName = GetObjectFullName(attr.Name, currentObjectName);
                                lbFullObjectsList.Items.Remove(fullObjectName);
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

        private void btClear_Click(object sender, EventArgs e)
        {
            if (furniture.IsDictionaryEmpty())
            {
                MessageBox.Show("Список объектов пуст.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                switch (MessageBox.Show("Список объектов будет очищен и информация, содержащаяся в нем, будет потеряна. " +
                    "Вы уверены, что хотите продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        furniture.ClearDictionary();
                        cmbObjects.Items.Clear();
                        lbFullObjectsList.Items.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        private void miAboutProgram_Click(object sender, EventArgs e)
        {
            MessageBox.Show("           Программа представляет собой простой CRUD (Create, Read, Update, Delete) " +
                "редактор объектов с хранением данных в оперативной памяти. Возможно добавление не более " +
                MAX_NUMBER_OF_OBJECTS.ToString() + " объектов одного типа.\n" +
                "           Также имеется возможность сохранения и загрузки списка объектов в файл/из файла.", 
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miAboutDeveloper_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа была разработана Ильиной Александрой, \n" +
                "студенткой группы 851001", "О разработчике", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miTaskTwo_Click(object sender, EventArgs e)
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

        private void miAdditionalTaskTwo_Click(object sender, EventArgs e)
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

        private void miTaskThree_Click(object sender, EventArgs e)
        {
            MessageBox.Show("           Задание к лабораторной работе 3 \"Сериализация\"\n\n" +
                "           В программу, разработанную во второй работе, необходимо добавить возможность сохранения и загрузки " + 
                "списка объектов в файл/из файла.\n\n" +
                "           Необходимо реализовать следующие виды сериализации/десериализации:\n\n" +
                "   - бинарная (с использованием готовых библиотечных функций для сериализации);\n" +
                "   - XML/JSON на выбор (с использованием готовых библиотечных функций для сериализации);\n" +
                "   - в произвольном текстовом формате (без использования готовых библиотечных функций для сериализации).\n\n" +
                "           Выбор типа сериализации должен выполняться в диалоговом окне сохранения/открытия файла.\n\n" +
                "           Все сериализаторы должен реализовывать общий интерфейс. Выбор и использование сериализатора следует " + 
                "реализовать таким образом, чтобы добавление нового сериализатора не требовало изменения существующего кода " + 
                "(выбор формата с помощью оператора switch/case делать нельзя).\n\n",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void miTaskFour_Click(object sender, EventArgs e)
        {
            MessageBox.Show("           Задание к лабораторной работе 4 \"Плагины\"\n\n" +
                "           Необходимо на базе предыдущей работе реализовать минимум 2 плагина для обработки данных перед " + 
                "сохранением в файл.\n\n" +
                "           В зависимости от варианта выбрать тип обработки:\n\n" +
                "   1. Кодирование в Base64 Base58, Base32, ZBase32.\n" +
                "   2. Архивация.\n" +
                "   3. Шифрование.\n\n" +
                "           Варианты выдаются последовательно в соответствии со списком группы.\n\n" +
                "           При реализации алгоритмов обработки допускается использование сторонних библиотек.\n\n" +
                "           Загрузка плагинов должна производиться автоматически из папки.\n\n" +
                "           При сохранении файла тип обработки (конкретный алгоритм кодирования, архивации, шифрования) должен " + 
                "выбираться из списка, который зависит от загруженных плагинов.\n\n" +
                "           При открытии файла тип обработки должен выбираться автоматически согласно тому, какая обработка " + 
                "проводилась перед сохранением. Допускается определение типа обработки по расширению файла. " + 
                "Если соответствующий плагин не загружен, вывести сообщение об ошибке.\n\n",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!furniture.IsDictionaryEmpty())
            {
                switch (MessageBox.Show("Вы уверены, что хотите выйти? Несохраненные данные будут потеряны.", "Предупреждение",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void lbFullObjectsListUpdate()
        {
            lbFullObjectsList.Items.Clear();
            foreach (KeyValuePair<Type, Dictionary<string, object>> firstKeyValue in furniture.furniture)
            {
                NameAttribute attr = (NameAttribute)firstKeyValue.Key.GetCustomAttribute(typeof(NameAttribute));
                foreach (KeyValuePair<string, object> secondKeyValue in firstKeyValue.Value)
                {
                    FieldInfo fieldNew = secondKeyValue.Value.GetType().GetField("Name");
                    String currentObjectName = (String)fieldNew.GetValue(secondKeyValue.Value);
                    String fullObjectName = GetObjectFullName(attr.Name, currentObjectName);
                    lbFullObjectsList.Items.Add(fullObjectName);
                }
            }
        }

        private void miFileOpen_Click(object sender, EventArgs e)
        {
            BooleanRef IsChanged = new BooleanRef();
            FormSerialization serializationForm = new FormSerialization(classesSerializationTypesLibrary, attributesSerialization, 
                furniture, FormSerializationToDo.Open, classesAssemblySerialization, IsChanged);
            serializationForm.ShowDialog();
            if (IsChanged.value)
            {
                lbFullObjectsListUpdate();
                SetComboBoxOnFirst(cmbClasses);
                cmbObjectsUpdate();
            }
        }

        private void miFileSave_Click(object sender, EventArgs e)
        {
            BooleanRef IsChanged = new BooleanRef();
            if (furniture.IsDictionaryEmpty())
            {
                MessageBox.Show("Список объектов пуст. Нельзя сохранить пустой список объектов.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                FormSerialization serializationForm = new FormSerialization(classesSerializationTypesLibrary, attributesSerialization, 
                    furniture, FormSerializationToDo.Save, classesAssemblySerialization, IsChanged);
                serializationForm.ShowDialog();
            }

        }

    }
}
