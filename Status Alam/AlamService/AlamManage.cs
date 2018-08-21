﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    class AlamManage
    {
        public int Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                ChangeFormSize();
                ChangeDeskLocation();
            }
        }
        private int _Size = 0;
        private List<Alam> AlamList = new List<Alam>();
        public Point Padding
        {
            get
            {
                return _Padding;
            }
            set
            {
                _Padding = Padding;
                ChangeDeskLocation();
            }
        }
        private Point _Padding = new Point(30, 10);
        Form Form = null;

        
        private void ChangeFormSize()
        {
            Form.Size = new Size(Form.Size.Width,
                                200 * (AlamList.Count + 1));
        }
        private void ChangeDeskLocation()
        {
            Rectangle pt = Screen.PrimaryScreen.WorkingArea;
            Form.SetDesktopLocation(pt.Width - this.Form.Width - _Padding.X, pt.Height - Form.Size.Height - _Padding.Y);
        }
        private void Remove(object sender, AlamStruct AlamStruct)
        {
            AlamList.Remove(sender as Alam);

            ChangeFormSize();
            ChangeDeskLocation();

            (sender as Control).Dispose();
        }

        public AlamManage(Form _Form)
        {
            Form = _Form;
            Size = 200;
        }

        public void Add(AlamStruct AlamStruct)
        {
            int v_Y = Size;
            int v_X = Form.Size.Width;

            Alam alam = new Alam(AlamStruct);
            alam.LifeTimeEnd += Remove;
            AlamList.Add(alam);
            
            for (int i = 0; i < AlamList.Count; i++)
            {
                AlamList[i].Location = new Point(0, Form.Size.Height - ((i + 1) * v_Y));
                AlamList[i].Size = new Size(Form.Size.Width, v_Y);
            }
            Form.Controls.Add(alam);
        }

    }



    public class AlamStruct
    {
        public AlamStruct(string Title, string Body, float LifeTime = 0.0f)
        {
            this.Title = Title;
            this.Body = Body;
            this.LifeTime = LifeTime;
        }

        /// <summary>
        /// 알람의 제목을 설정합니다.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 알람의 주 내용을 설정합니다.
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// 이미지 경로설정.
        /// </summary>
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// 백컬러 지정.
        /// </summary>
        public Color BackColor { get; set; } = Color.Empty;

        public BorderStyle borderStyle { get; set; } = BorderStyle.None;

        /// <summary>
        /// 표기할 시간을 지정합니다. ( 0.0f => infinity )
        /// </summary>
        public float LifeTime { get; set; } = 0.0f;

    }
}