﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace NoughtsAndCrosses
{
    public class Window : Form
    {
        private Action<Graphics> onPaint;
        private Action onTick;
        private Action<Point> onClick;
        private Action<Keys> onKeyDown;
        private Action<Point> onMouseMove;

        private Timer timer = new Timer();
		
        public Window(int width, int height, string title)
        {
            Text = title;
            this.Width = width;
            this.Height = height;
			
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.LightCyan;
            
			// Commented out for Mono compatibility.
			// Temporary
			// ...Hopefully...
			//Icon = Properties.Resources.Icon;
				
            Click += new EventHandler(OnClick);
			
			int fps = 60;
            timer.Interval = 1000 / fps; // Tick at 60 FPS (16.6.... ms per frame)
            timer.Tick += new EventHandler(OnTick);

            DoubleBuffered = true;
            CenterToScreen();
        }

        public void SetPaintFunction(Action<Graphics> function)
        {
            onPaint = function;
        }

        public void SetTickFunction(Action function)
        {
            onTick = function;
            timer.Start();
        }

        public void SetClickFunction(Action<Point> function)
        {
            onClick = function;
        }

        public void SetKeyboardFunction(Action<Keys> function)
        {
            onKeyDown = function;
        }

        public void SetMoveMoveFunction(Action<Point> function)
        {
            onMouseMove = function;
        }

        private void OnTick(object sender, EventArgs args)
        {
            Point mousePoint = new Point(MousePosition.X, MousePosition.Y);
            mousePoint = PointToClient(mousePoint);
            Global.MousePos = mousePoint;

            onTick();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            onPaint(e.Graphics);
        }

        private void OnClick(object sender, EventArgs args)
        {
            Point mousePoint = new Point(MousePosition.X, MousePosition.Y);
            mousePoint = PointToClient(mousePoint);
            onClick(mousePoint);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            onKeyDown(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            onMouseMove(e.Location);
            base.OnMouseMove(e);
        }
    }
}
