using MVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MVVM.ViewModels
{
	public class FriendViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		FriendsListViewModel lvm;
		public Friend Friend { get; set; }
		public FriendViewModel() { Friend = new Friend(); }
		public FriendsListViewModel ListViewModel 
		{
			get { return lvm; } 
			set { if (lvm != value) 
				{
					lvm = value;
					OnPropertyChanged("ListViewModel");
				} 
			} 
		}
		public string Name
		{
			get { return Friend.Name; }
			set
			{
				if (Friend.Name != value)
				{
					Friend.Name = value;
					OnPropertyChanged("Name");
				}
			}
		}
		public string Email
		{
			get { return Friend.Email; }
			set
			{
				if (Friend.Email != value)
				{
					Friend.Email = value;
					OnPropertyChanged("Email");
				}
			}
		}
		public string Phone
		{
			get { return Friend.Phone; }
			set
			{
				if (Friend.Phone != value)
				{
					Friend.Phone = value;
					OnPropertyChanged("Phone");
				}
			}
		}
		public bool IsValid
		{
			get
			{
				return
					(!string.IsNullOrEmpty(Name.Trim())) ||
					(!string.IsNullOrEmpty(Email.Trim())) || 
					(!string.IsNullOrEmpty(Phone.Trim()));
			}
		}
		private void OnPropertyChanged( string v)
		{
			if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(v)); }
		}
	}
}
