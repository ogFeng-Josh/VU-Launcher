﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VULauncher.ViewModels.Common;
using VULauncher.ViewModels.Items.Common;
using VULauncher.ViewModels.SettingsViewModels;

namespace VULauncher.ViewModels
{
    public class SettingsViewModel : ItemsParentViewModel
    {
        // TODO: having bound the changing of tabs to a tabindex and using the ITabViewModel interface for that is kinda ugly since it lows too little control over the active VM

        private int _tabIndex;

        public ClientParamsViewModel ClientParamsViewModel { get; set; }
        public ClientPresetsViewModel ClientPresetsViewModel { get; set; }

        public ServerParamsViewModel ServerParamsViewModel { get; set; }
        public MapListsViewModel MapListsViewModel { get; set; }
        public StartupsViewModel StartupsViewModel { get; set; }
        public BanListsViewModel BanListsViewModel { get; set; }
        public ServerPresetsViewModel ServerPresetsViewModel { get; set; }

        public List<IPresetTabViewModel> TabViewModels { get; }

        public int TabIndex
        {
            get => _tabIndex;
            set => SetField(ref _tabIndex, value);
        }

        public SettingsViewModel()
        {
            ClientParamsViewModel = new ClientParamsViewModel();
            ClientPresetsViewModel = new ClientPresetsViewModel(ClientParamsViewModel);

            ServerParamsViewModel = new ServerParamsViewModel();
            MapListsViewModel = new MapListsViewModel();
            StartupsViewModel = new StartupsViewModel();
            BanListsViewModel = new BanListsViewModel();
            ServerPresetsViewModel = new ServerPresetsViewModel(ServerParamsViewModel, MapListsViewModel, StartupsViewModel, BanListsViewModel);

            RegisterChildItem(ClientParamsViewModel);
            RegisterChildItem(ClientPresetsViewModel);
            RegisterChildItem(ServerParamsViewModel);
            RegisterChildItem(MapListsViewModel);
            RegisterChildItem(StartupsViewModel);
            RegisterChildItem(BanListsViewModel);
            RegisterChildItem(ServerPresetsViewModel);

            TabViewModels = new List<IPresetTabViewModel>()
            {
                ClientPresetsViewModel,
                ServerPresetsViewModel,
                ClientParamsViewModel,
                ServerParamsViewModel,
                MapListsViewModel,
                StartupsViewModel,
                BanListsViewModel,
            };

            foreach (var tabViewModel in TabViewModels)
            {
                tabViewModel.TabIndexChanged += TabViewModel_OnTabIndexChanged;
            }
        }

        private void TabViewModel_OnTabIndexChanged(object sender, TabIndexChangedEventArgs e)
        {
            TabIndex = e.NewTabIndex;
            var currentTabViewModel = TabViewModels[TabIndex];
            currentTabViewModel.SetSelectedPreset(e.SelectedPresetId);
        }

        public void SaveTab()
        {
            var currentTabViewModel = TabViewModels[TabIndex];
            currentTabViewModel.Save();
        }

        public void SaveAllTabs()
        {
            TabViewModels.ForEach(t => t.Save());
        }
    }
}
