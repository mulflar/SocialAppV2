using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SocialAppV2.ViewModels;

namespace SocialAppV2.View
{
    public partial class VistaPlanes : UserControl
    {
        public VistaPlanes()
        {
            InitializeComponent();
        }
    }
    public class PlanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Fiestas
        {
            get;
            set;
        }

        public DataTemplate UnHealthy
        {
            get;
            set;
        }

        public DataTemplate NotDetermined
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            /*Data foodItem = item as Data;
            if (foodItem != null)
            {
                if (foodItem.Type == "Healthy")
                {
                    return Healthy;
                }
                else if (foodItem.Type == "NotDetermined")
                {
                    return NotDetermined;
                }
                else
                {
                    return UnHealthy;
                }
            }*/

            return base.SelectTemplate(item, container);
        }
    }
}
