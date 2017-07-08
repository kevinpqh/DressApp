﻿using DressApp.Model.ClothingItems;
using DressApp.ViewModel.ButtonItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace DressApp.ViewModel
{
    //view model para la visra
    public class KinectViewModel : ViewModelBase
    {
        #region Atributos Privados

        //clothing manager
        private ClothingManager _clothingManager;

        //kinect Services
        private readonly KinectService _kinectService;

        #endregion

        #region Propiedades Publicas
        // Get el button player.
        public static SoundPlayer ButtonPlayer { get; private set; }
        
        // Get o set el clothing manager.
        ClothingManager ClothingManager
        {
            get { return _clothingManager; }
            set
            {
                if (_clothingManager == value)
                    return;
                _clothingManager = value;
                OnPropertyChanged("ClothingManager");
            }
        }

        // Gets the kinect service.
        public KinectService KinectService
        {
            get { return _kinectService; }
        }

        public bool DebugModeOn
        {
            get
            {
#if DEBUG
                return true;
#endif
                return false;
            }
        }
        #endregion

        #region Constructor
        public KinectViewModel(KinectService kinectService)
        {
            ButtonPlayer = new SoundPlayer(Properties.Resources.ButtonClick);
            InitializeClothingCategories();
            _kinectService = kinectService;
            _kinectService.Initialize();
        }
        #endregion

        #region Metodos Privados

        // inicializa las categorias de Ropa
        private void InitializeClothingCategories()
        {
            ClothingManager.Instance.ClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>
            {
                CreateHatsClothingCategoryButton(),
                CreateTiesClothingCategoryButton(),
                CreateSkirtsClothingCategoryButton(),
                CreateDressesClothingCategoryButton(),
                CreateGlassesClothingCategoryButton(),
                CreateTopsClothingCategoryButton(),
                CreateBagsClothingCategoryButton()
            };
            ClothingManager.Instance.UpdateActualCategories();
        }

        // Crea el botón categorias de sombreros.
        // Retorna botones de las categorias de sombreros
        private ClothingCategoryButtonViewModel CreateHatsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Male)
            {
                Image = Properties.Resources.hat_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\cowboy_hat.obj")
                    { Image = Properties.Resources.cowboy_hat }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\cowboy_hat_straw.obj")
                    { Image = Properties.Resources.cowboy_straw }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem , @".\Resources\Models\Hats\cowboy_hat_gray.obj")
                    { Image = Properties.Resources.cowboy_dark }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem , @".\Resources\Models\Hats\fedora_brown_hat.obj")
                    { Image = Properties.Resources.fedora_hat_brown, Ratio = 0.6 }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\fedora_darkgreen_hat.obj")
                    { Image = Properties.Resources.fedora_hat_darkgreen, Ratio = 0.6 }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\fedora_hat.obj")
                    { Image = Properties.Resources.fedora_hat_black, Ratio = 0.6 }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\hat_brown.obj")
                    { Image = Properties.Resources.hat_brown, Ratio = 1 }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\hat_black.obj")
                    { Image = Properties.Resources.hat_black, Ratio = 1 }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\hat_white.obj")
                    { Image = Properties.Resources.hat_white, Ratio = 1 }
                }
            };
        }
        
        // Crea el botón categorias de faldas.
        // Retorna botones de las categorias de faldas
        private ClothingCategoryButtonViewModel CreateSkirtsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Female)
            {
                Image = Properties.Resources.skirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\medium_skirt.obj")
                    { Image = Properties.Resources.medium_skirt }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\medium_skirt_checked.obj")
                    { Image = Properties.Resources.medium_skirt_checked }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\medium_skirt_denim.obj")
                    { Image = Properties.Resources.medium_skirt_denim }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\medium_skirt_green_stripes.obj")
                    { Image = Properties.Resources.medium_skirt_green_stripes }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\medium_skirt_violet.obj")
                    { Image = Properties.Resources.medium_skirt_violet }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\medium_skirt_red.obj")
                    { Image = Properties.Resources.medium_skirt_red }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\medium_skirt_navy.obj")
                    { Image = Properties.Resources.medium_skirt_navy }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt.obj")
                    { Image = Properties.Resources.long_skirt, BottomJointToTrackScale = JointType.FootLeft }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt_bikes.obj")
                    { Image = Properties.Resources.long_skirt_bikes, BottomJointToTrackScale = JointType.FootLeft }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt_indian.obj")
                    { Image = Properties.Resources.long_skirt_indian, BottomJointToTrackScale = JointType.FootLeft }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt_violet.obj")
                    { Image = Properties.Resources.long_skirt_violet, BottomJointToTrackScale = JointType.FootLeft }
                     , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\big_skirt_green.obj")
                    { Image = Properties.Resources.breezy_skirt_winter, BottomJointToTrackScale = JointType.FootLeft }
                     , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\big_skirt_waves.obj")
                    { Image = Properties.Resources.breezy_skirt_waves, BottomJointToTrackScale = JointType.FootLeft }
                     , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\big_skirt_red.obj")
                    { Image = Properties.Resources.breezy_skirt_red, BottomJointToTrackScale = JointType.FootLeft }
                     , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\big_skirt_luma.obj")
                    { Image = Properties.Resources.bolsokevin, BottomJointToTrackScale = JointType.FootLeft }
                 

                     
                }
            };
        }
        
        // Crea el botón categorias de vestidos.
        // Retorna botones de las categorias de vestidos
        private ClothingCategoryButtonViewModel CreateDressesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Female)
            {
                Image = Properties.Resources.dress_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_dark.obj")
                    { Image = Properties.Resources.dress_dark }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_green.obj")
                    { Image = Properties.Resources.dress_green }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_navy.obj")
                    { Image = Properties.Resources.dress_navy }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_red.obj")
                    { Image = Properties.Resources.dress_red, Ratio = 1.8, DeltaY = 0.95 }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_darkgreen.obj")
                    { Image = Properties.Resources.dress_darkgreen, Ratio = 1.8, DeltaY = 0.95 }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_orange.obj")
                    { Image = Properties.Resources.dress_orange, Ratio = 1.8, DeltaY = 0.95 }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_violet.obj")
                    { Image = Properties.Resources.dress_violet }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_blue.obj")
                    { Image = Properties.Resources.dress_blue }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_sunny.obj")
                    { Image = Properties.Resources.dress_sunny }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_pink.obj")
                    { Image = Properties.Resources.dress_pink }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_kitchen.obj")
                    { Image = Properties.Resources.dress_kitchen }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_egipt.obj")
                    { Image = Properties.Resources.dress_egipt }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_beige.obj")
                    { Image = Properties.Resources.dress_beige }
                }
            };
        }
        
        // Crea el botón categorias de lentes.
        // Retorna botones de las categorias de lentes
        private ClothingCategoryButtonViewModel CreateGlassesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Both)
            {
                Image = Properties.Resources.glasses_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_sea.obj")
                        { Image = Properties.Resources.glass_sea }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_maroon.obj")
                        { Image = Properties.Resources.glass_maroon }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_yellow.obj")
                        { Image = Properties.Resources.glass_yellow }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_blue.obj")
                        { Image = Properties.Resources.glass_blue }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_red.obj")
                        { Image = Properties.Resources.glass_red }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\sunglass_green.obj")
                        { Image = Properties.Resources.sunglass_green }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\sunglass_orange.obj")
                        { Image = Properties.Resources.sunglass_orange }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\sunglass_violet.obj")
                        { Image = Properties.Resources.sunglass_violet }
                    }
            };
        }
        
        // Crea el botón categorias de corbatas.
        // Retorna botones de las categorias de corbatas
        private ClothingCategoryButtonViewModel CreateTiesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Male)
            {
                Image = Properties.Resources.tie_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_beige.obj")
                        { Image = Properties.Resources.tie_beige }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_blue.obj")
                        { Image = Properties.Resources.tie_blue }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_colourful.obj")
                        { Image = Properties.Resources.tie_color }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_green.obj")
                        { Image = Properties.Resources.tie_green }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_gray.obj")
                        { Image = Properties.Resources.tie_gray }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_stripes.obj")
                        { Image = Properties.Resources.tie_stripes }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_dark.obj")
                        { Image = Properties.Resources.tie_dark }
                    }
            };
        }
        
        // Crea el botón categorias de carteras.
        // Retorna botones de las categorias de carteras
        private ClothingCategoryButtonViewModel CreateBagsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Both)
            {
                Image = Properties.Resources.bag_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\armani_bag_beige.obj")
                        { Image = Properties.Resources.armani_bag_beige }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\armani_bag_camel.obj")
                        { Image = Properties.Resources.armani_bag_camel }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\armani_bag_white.obj")
                        { Image = Properties.Resources.armani_bag_white }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\men_bag_brown.obj")
                        { Image = Properties.Resources.men_bag_brown }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\men_bag_brown2.obj")
                        { Image = Properties.Resources.men_bag_brown2 }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\men_bag_gray.obj")
                        { Image = Properties.Resources.men_bag_gray }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\small_bag_beige.obj")
                        { Image = Properties.Resources.small_bag_beige }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\small_bag_camel.obj")
                        { Image = Properties.Resources.small_bag_camel }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\small_bag_brown.obj")
                        { Image = Properties.Resources.small_bag_brown }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\small_bag_gray.obj")
                        { Image = Properties.Resources.small_bag_gray }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\sport_bag_red.obj")
                        { Image = Properties.Resources.sport_bag_red }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\sport_bag_blue.obj")
                        { Image = Properties.Resources.sport_bag_blue }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\sport_bag_green.obj")
                        { Image = Properties.Resources.sport_bag_green }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\suitcase_brown.obj")
                        { Image = Properties.Resources.suitcase_brown }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\suitcase_brown2.obj")
                        { Image = Properties.Resources.suitcase_brown2 }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\suitcase_gray.obj")
                        { Image = Properties.Resources.suitcase_gray }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\Backpack.obj")
                        { Image = Properties.Resources.bolsokevin }
                    }
            };
        }

        // Crea el botón categorias de polos.
        // Retorna botones de las categorias de polos
        private ClothingCategoryButtonViewModel CreateTopsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Both)
            {
                Image = Properties.Resources.top_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\big_top_colourful.obj")
                    { Image = Properties.Resources.big_top_colourful, DeltaY = 0.9 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\big_top_dark.obj")
                    { Image = Properties.Resources.big_top_dark, DeltaY = 0.9 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\big_top_green.obj")
                    { Image = Properties.Resources.big_top_green, DeltaY = 0.9 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\long_top_blue.obj")
                    { Image = Properties.Resources.long_top_blue, BottomJointToTrackScale = JointType.HipCenter }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\long_top_pink.obj")
                    { Image = Properties.Resources.long_top_pink, BottomJointToTrackScale = JointType.HipCenter }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\top_blue.obj")
                    { Image = Properties.Resources.top_blue, Ratio = 1, DeltaY = 0.9 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\top_red.obj")
                    { Image = Properties.Resources.top_red, Ratio = 1, DeltaY = 0.9 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\top2_green.obj")
                    { Image = Properties.Resources.top_green, DeltaY = 0.9 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Tops\top2_orange.obj")
                    { Image = Properties.Resources.top_orange, DeltaY = 0.9 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Tops\tshirt_navy.obj")
                    { Image = Properties.Resources.tshirt_navy, Ratio = 1.45 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Tops\tshirt_green_blue.obj")
                    { Image = Properties.Resources.tshirt_green_blue, Ratio = 1.45 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Tops\tshirt_coral_blue.obj")
                    { Image = Properties.Resources.tshirt_coral_blue, Ratio = 1.45 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Tops\tshirt_olive.obj")
                    { Image = Properties.Resources.tshirt_olive, Ratio = 1.45 }
                    , new TopButtonViewModel(ClothingItemBase.ClothingType.TopItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Tops\tshirt_orange_blue.obj")
                    { Image = Properties.Resources.tshirt_orange_blue, Ratio = 1.45 }
                }
            };
        }
        #endregion


    }
}
