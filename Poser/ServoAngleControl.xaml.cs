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

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Interaction logic for ServoControl.xaml
    /// </summary>
    public partial class ServoAngleControl : UserControl, ServoInteractive
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Members
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Servo servo;



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ServoAngleControl()
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            InitializeComponent();

            angleSlider.Slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(angleSliderValueChanged);

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void angleSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (servo != null)
                servo.MoveToAngle(angleSlider.Slider.Value);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetServo(Servo value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            servo = value;

            servo.Changed += new ServoDelegate(servo_Changed);

            servo_Changed(servo);


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void servo_Changed(Servo value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            Dispatcher.Invoke(new ServoDelegate(SyncDisplay), servo);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void SyncDisplay(Servo value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            nameLabel.Content = servo.Name;

            angleSlider.Slider.Maximum      = servo.Range.maximum;
            angleSlider.Slider.Minimum      = servo.Range.minimum;
            angleSlider.MaxBound            = servo.Stops.maximum;
            angleSlider.MinBound            = servo.Stops.minimum;
            angleSlider.Slider.Value        = servo.Target;
            angleSlider.slider.Reference    = servo.Angle;

        }

    }
}
