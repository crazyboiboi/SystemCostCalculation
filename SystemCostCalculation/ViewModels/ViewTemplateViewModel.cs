using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCostCalculation.HelperClasses;
using SystemCostCalculation.Models;

namespace SystemCostCalculation.ViewModels
{
    public class ViewTemplateViewModel : ViewModelBase
    {

        #region Field
        public ObservableCollection<TemplateModel> templates
        {
            get
            {
                return Constants.templates;
            }
        }

        private TemplateModel _selectedTemplate;
        public TemplateModel selectedTemplate
        {
            get
            {
                return _selectedTemplate;
            }
            set
            {
                Set(ref _selectedTemplate, value);
                EditTemplateCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region UI Command
        private RelayCommand editTemplateCommand; 
        public RelayCommand EditTemplateCommand
        {
            get
            {
                if (editTemplateCommand == null)
                {
                    editTemplateCommand = new RelayCommand(() =>
                    {
                        Console.WriteLine(_selectedTemplate.TemplateSaveName);
                        TemplateModel templateToEdit = TemplateSaveAndLoad.load(_selectedTemplate.TemplateSaveName);
                        Constants.currentTemplate = templateToEdit;
                        Messenger.Default.Send<SwitchViewMessage>(new SwitchViewMessage { ViewName = "createtemplate" });
                        Constants.createTemplateViewModel.PopulateTemplate();
                    },
                    () => _selectedTemplate != null);
                }
                return editTemplateCommand;
            }
        }

        private RelayCommand deleteTemplateComamnd; 
        public RelayCommand DeleteTemplateCommand
        {
            get
            {
                if (deleteTemplateComamnd == null)
                {
                    deleteTemplateComamnd = new RelayCommand(() =>
                    {
                        DeleteTemplate();
                    },
                    () => _selectedTemplate != null);
                }
                return deleteTemplateComamnd;
            }
        }


        #endregion


        private void DeleteTemplate()
        {

        }




        public ViewTemplateViewModel()
        {

        }

    }
}
