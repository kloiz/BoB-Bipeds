﻿/*
//    FOBO Poser application
//      This program allows FOBO to be posed into different positions.
//
//    Copyright (C) 2012  Jonathan Dowdall, Project Biped (www.projectbiped.com)
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServoControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Interaction logic for ActionControl.xaml
    /// </summary>
    public partial class ActionControl : UserControl
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Members
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Action action;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Properties
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public Action Action { get { return action; } set { SetAction(value); } }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionControl()
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetAction(Action value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            action = value;

            nameBox.Text            = action.Name;
            delayLabel.Text         = ""+ action.Delay;
            loopCheckBox.IsChecked  = action.Loop;
            framesLabel.Content     = "" + action.Frames.Count;

            action.FrameAdded   += new FrameDelegate(action_FrameAdded);
            action.FrameRemoved += new FrameDelegate(action_FrameRemoved);
            action.DelayChanged += new IntDelegate(action_DelayChanged);
            action.LoopChanged  += new BoolDelegate(action_LoopChanged);

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void action_LoopChanged(bool value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            loopCheckBox.IsChecked = value;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void action_DelayChanged(int value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            delayLabel.Text = "" + value;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void action_FrameRemoved(Frame frame)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            framesLabel.Content = ""+action.Frames.Count;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void action_FrameAdded(Frame frame)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            framesLabel.Content = "" + action.Frames.Count;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void delayLabel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            try
            {
                int.Parse(e.Text);
            }
            catch(Exception exception)
            {
                e.Handled = true;
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void delayLabel_TextChanged(object sender, TextChangedEventArgs e)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (action != null)
            {
                try
                {
                    action.Delay = int.Parse(delayLabel.Text);
                }
                catch (Exception exception) { }
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void loopCheckBox_Checked(object sender, RoutedEventArgs e)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (action != null)
                action.Loop = (bool)loopCheckBox.IsChecked;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void loopCheckBox_Unchecked(object sender, RoutedEventArgs e)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (action != null)
                action.Loop = (bool)loopCheckBox.IsChecked;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {

            if(action != null)
                action.Name = nameBox.Text;
        }
    }
}
