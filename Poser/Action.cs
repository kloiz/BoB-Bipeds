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
using System.Xml.Serialization;
using System.IO;

namespace ServoControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class Action
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {

        public List<FrameOld> oldFrames;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Members
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<Frame> frames;
            bool        loop;
            int         delay;
            string      name;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Proerties
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public List<Frame>  Frames  { get { return frames; } }
            public bool         Loop    { get { return loop; }  set { SetLoop(value); } }
            public int          Delay   { get { return delay; } set { SetDelay(value); } }
            public string       Name    { get { return name; } set { SetName(value); } }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Events
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public event FrameDelegate  FrameAdded;
            public event FrameDelegate  FrameRemoved;
            public event FrameDelegate  FrameChanged;
            public event IntDelegate    DelayChanged;
            public event BoolDelegate   LoopChanged;
            public event StringDelegate NameChanged;



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Action()
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            //create the frames list
            frames = new List<Frame>();

            //default delay
            delay = 25;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetLoop(bool value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (loop != value)
            {
                loop = value;
                if (LoopChanged != null)
                    LoopChanged(loop);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetDelay(int value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (delay != value)
            {
                delay = value;
                if (DelayChanged != null)
                    DelayChanged(delay);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetName(string value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (name == null || name.CompareTo(value) != 0)
            {
                name = value;
                if (NameChanged != null)
                    NameChanged(name);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void InsertFrame(Frame frame, int index)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            //insert the frame
            if (index >= 0 && index <= frames.Count)
            {
                frames.Insert(index, frame);

                //trigger event
                if (FrameAdded != null)
                    FrameAdded(frame);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddFrame(Frame frame)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            //add the frame
            frames.Add(frame);

            //trigger event
            if (FrameAdded != null)
                FrameAdded(frame);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveFrame(Frame frame)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (frames.Contains(frame))
            {
                //add the frame
                frames.Remove(frame);

                //trigger event
                if (FrameRemoved != null)
                    FrameRemoved(frame);
            }
        }


    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class Frame
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Members
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            double[] state;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Properties
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public double[] State { get { return state; } set { state = value; } }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Events
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public event IntDelegate ValueChanged;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Frame()
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetValue(int index, double value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (state != null && index < state.Length)
                state[index] = value;

            if (ValueChanged != null)
                ValueChanged(index);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class FrameOld
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Members
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        byte[] state;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Properties
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public byte[] State { get { return state; } set { state = value; } }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Events
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public event IntDelegate ValueChanged;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public FrameOld()
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetValue(int index, byte value)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (state != null && index < state.Length)
                state[index] = value;

            if (ValueChanged != null)
                ValueChanged(index);
        }



    }
}


