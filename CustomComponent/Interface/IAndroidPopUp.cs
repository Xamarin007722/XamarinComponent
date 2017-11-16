using System;
namespace XamarinComponent.Interface
{
    public interface IAndroidPopUp
    {
        void ShowToast(string message);
        void ShowSnackbar(string message);
    }
}
