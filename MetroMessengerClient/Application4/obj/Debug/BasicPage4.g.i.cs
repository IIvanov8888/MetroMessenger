﻿

#pragma checksum "C:\Users\mitko\Documents\Visual Studio 11\Projects\Application4\Application4\BasicPage4.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BDAF5B34D1D18140F8FD362E6BEAD319"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Application4
{
    public partial class BasicPage4 : Application4.Common.LayoutAwarePage
    {
        private Application4.Common.LayoutAwarePage pageRoot;
        private Windows.UI.Xaml.Controls.TextBox MessageText;
        private Windows.UI.Xaml.Controls.TextBlock ChatText;
        private Windows.UI.Xaml.Controls.Button backButton;
        private Windows.UI.Xaml.Controls.TextBlock pageTitle;
        private Windows.UI.Xaml.VisualState FullScreenLandscape;
        private Windows.UI.Xaml.VisualState Filled;
        private Windows.UI.Xaml.VisualState FullScreenPortrait;
        private Windows.UI.Xaml.VisualState Snapped;
        private bool _contentLoaded;

        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            Application.LoadComponent(this, new System.Uri("ms-appx:///BasicPage4.xaml"), Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            pageRoot = (Application4.Common.LayoutAwarePage)this.FindName("pageRoot");
            MessageText = (Windows.UI.Xaml.Controls.TextBox)this.FindName("MessageText");
            ChatText = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("ChatText");
            backButton = (Windows.UI.Xaml.Controls.Button)this.FindName("backButton");
            pageTitle = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("pageTitle");
            FullScreenLandscape = (Windows.UI.Xaml.VisualState)this.FindName("FullScreenLandscape");
            Filled = (Windows.UI.Xaml.VisualState)this.FindName("Filled");
            FullScreenPortrait = (Windows.UI.Xaml.VisualState)this.FindName("FullScreenPortrait");
            Snapped = (Windows.UI.Xaml.VisualState)this.FindName("Snapped");
        }
    }
}



