using System;
using PropertyChanged;
using SQLite;
using UtilityClasses.Utility;

namespace CustomComponent.SearchNearBy.Models
{
    public class SettingsDataModel:BaseDisposable
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string FirstBtnText { get; set; }
        public string SecondBtnText { get; set; }
        public string ThirdBtnText { get; set; }
        public string FourthBtnText { get; set; }

        public string FirstSearchText { get; set; }
        public string SecondSearchText { get; set; }
        public string ThirdSearchText { get; set; }
        public string FourthSearchText { get; set; }

    }

    [AddINotifyPropertyChangedInterface]
    public class SettingsModel
    {
        string _entryFirstText;
        public string EntryFirstText
        {
            get { return _entryFirstText; }
            set { _entryFirstText = value; }
        }

        string _entrySecondText;
        public string EntrySecondText
        {
            get { return _entrySecondText; }
            set { _entrySecondText = value; }
        }

        string _entryThirdText;
        public string EntryThirdText
        {
            get { return _entryThirdText; }
            set { _entryThirdText = value; }
        }
        string _entryFoutyhText;
        public string EntryFourthText
        {
            get { return _entryFoutyhText; }
            set { _entryFoutyhText = value; }
        }


        string _entryFirstSearchText;
        public string EntryFirstSearchText
        {
            get { return _entryFirstSearchText; }
            set { _entryFirstSearchText = value; }
        }

        string _entrySecondSearchText;
        public string EntrySecondSearchText
        {
            get { return _entrySecondSearchText; }
            set { _entrySecondSearchText = value; }
        }

        string _entryThirdSearchText;
        public string EntryThirdSearchText
        {
            get { return _entryThirdSearchText; }
            set { _entryThirdSearchText = value; }
        }
        string _entryFoutyhSearchText;
        public string EntryFourthSearchText
        {
            get { return _entryFoutyhSearchText; }
            set { _entryFoutyhSearchText = value; }
        }
    }

}
