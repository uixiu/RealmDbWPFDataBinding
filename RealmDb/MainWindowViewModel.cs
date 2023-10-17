using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using Realms;
using Realms.Exceptions;
using Realms.Sync;

namespace RealmDb
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel() 
        {
            var config = new RealmConfiguration(@"d:\temp\cats.realm");

           /* try
            {
                Realm.DeleteRealm(config);
            }
            catch(Exception ex){ }
*/
            LocalRealm = Realm.GetInstance(config);
            _myCatList = new CatList();

            Cat ninja = new() { Name = "Ninja", Age = 1, Breed = "Angora" };
            Cat nounou = new() { Name = "Nounou", Age = 2, Breed = "Siamese" };
            Cat leila = new() { Name = "Layla", Age = 3, Breed = "Local" };

            try
            {
                _myCatList.Items.Add(ninja);
                _myCatList.Items.Add(nounou);
                _myCatList.Items.Add(leila);

                // grouped writes trows "Range actions are not supported"
                LocalRealm.Write(() =>
                {
                    LocalRealm.Add(_myCatList);
                });

            }
            catch (RealmFileAccessErrorException ex)
            {
                MessageBox.Show($@"Error writeing to the realm file. {ex.Message}");
            }
           

           
         

            Cats =  _myCatList.Items.AsRealmQueryable<Cat>();
        }

        public Realm LocalRealm;

       
        private CatList? _myCatList;
     

        private IQueryable<Cat> _cats;
        public IQueryable<Cat> Cats
        {
            get => _cats;
            set => SetProperty(ref _cats, value);
        }
        public Cat? SelectedCat { get; set; }

       

        [RelayCommand]
        private void RemoveCat()
        {
            if (SelectedCat == null)
                return; 
    
            try
            {
                // throws exception: Attempting to access an invalid object
                LocalRealm.Write(() =>
                {
                    //Cat.Remove(SelectedCat);
                    _myCatList.Items.Remove(SelectedCat);

                });

                /*
                 Workaround: 
                Cats = null;
                Cats = LocalRealm.All<Cat>();*/
            }
            catch (Exception ex)
            {
                // the selected cat is not removed from the datagrid.
                // In the case of binding to a listbox. The deleted object still shows but it rises exception if selected.
                MessageBox.Show($@"Error deleting the selected cat. {ex.Message}");
            }
        }

        [RelayCommand]
        private void Populate()
        {
            
        }

    }
}
