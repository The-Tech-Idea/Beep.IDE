using Beep.IDE;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Beep.IDE.Winform.Controls
{
    public class BeepTabControl:TabControl
    {
        public event EventHandler<TabsDataEventarg> CloseButtonClick;
        public event EventHandler<TabsDataEventarg> TabRemoved;
        public event EventHandler<TabsDataEventarg> NextButtonClick;
        public event EventHandler<TabsDataEventarg> PrevButtonClick;
        public event EventHandler<TabsDataEventarg> FileIndexChanged;
        public RectangleF closeButton { get; set; } = new RectangleF();
        public RectangleF nextButton { get; set; } = new RectangleF();
        public RectangleF prevButton { get; set; } = new RectangleF();
        private Image closeButtonImage;
        private Image nextButtonImage;
        private Image prevButtonImage;
        public BeepTabControl()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.Padding = new Point(14, 4);
            this.DrawItem += BeepTabControl_DrawItem;
            this.MouseClick += BeepTabControl_MouseClick;
            this.TabIndexChanged += BeepTabControl_TabIndexChanged;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            //this.HandleCreated += (s, e) => RecalculateTabSizes();
            //this.ControlAdded += (s, e) => RecalculateTabSizes();
            //this.ControlRemoved += (s, e) => RecalculateTabSizes();
            //this.FontChanged += (s, e) => RecalculateTabSizes();

            closeButtonImage = global::Beep.IDE.Properties.Resources.close;
            nextButtonImage = global::Beep.IDE.Properties.Resources.Collapseright;
            prevButtonImage = global::Beep.IDE.Properties.Resources.CollapseLeft;
           
        }

        private void BeepTabControl_Disposed(object sender, EventArgs e)
        {
           
        }

        private void BeepTabControl_TabIndexChanged(object sender, EventArgs e)
        {
            
            FileIndexChanged?.Invoke(this, new TabsDataEventarg() { Tabidx = this.SelectedIndex });
        }
        private int GetTabIndexUnderMouse(Point location)
        {
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle tabRect = GetTabRect(i);
                if (tabRect.Contains(location))
                {
                    return i;
                }
            }
            return -1; // Return -1 if no tab is under the mouse
        }

        private void RecalculateTabSizes()
        {
            if (this.TabPages.Count == 0)
            {
                return;
            }

            int paddingWidth = 25; // Additional padding for the Close button
            int paddingHeight = 8; // Additional padding for the tab height
            int maxHeight = 0; // Keep track of the maximum height
            using (Graphics g = this.CreateGraphics())
            {
                int[] tabWidths = new int[this.TabPages.Count];

                for (int i = 0; i < this.TabPages.Count; i++)
                {
                    var tabPage = this.TabPages[i];
                    SizeF textSize = g.MeasureString(tabPage.Text, tabPage.Font);

                    int tabWidth = (int)textSize.Width + paddingWidth+20;
                    int tabHeight = (int)textSize.Height + paddingHeight+10;

                    if (tabHeight > maxHeight)
                    {
                        maxHeight = tabHeight;
                    }

                    tabWidths[i] = tabWidth;
                }

                for (int i = 0; i < this.TabPages.Count; i++)
                {
                    this.SizeMode = TabSizeMode.Fixed;
                    this.ItemSize = new Size(tabWidths[i], maxHeight);
                }
            }

            this.Invalidate(); // Redraw the control to apply the new tab sizes
        }
        private void BeepTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            int idx = -1;
            int tabIndex = GetTabIndexUnderMouse(e.Location);
            // Close button click
            // Check if the click is on the close button of any tab
            if (tabIndex != -1 && closeButton.Contains(e.Location))
            {
                if (closeButton.Contains(e.Location))
                {
                    System.Windows.Forms.TabPage tabPage = null;
                    for (int i = 0; i < this.TabCount; i++)
                    {
                        if (this.GetTabRect(i).Contains(e.Location))
                        {
                            tabPage = this.TabPages[i];
                            idx = i;
                            break;
                        }
                    }

                    if (tabPage != null)
                    {
                        TabsDataEventarg retval = new TabsDataEventarg() { Tabidx = idx, filename = tabPage.Text };
                        CloseButtonClick?.Invoke(this, new TabsDataEventarg() { Tabidx = idx, filename = tabPage.Text });
                        if (!retval.Cancel)
                        {
                            this.TabPages.Remove(tabPage);
                            TabRemoved?.Invoke(this, new TabsDataEventarg() { Tabidx = idx, filename = tabPage.Text });
                        }

                    }
                }
            }
            else
            {
                // Since Next and Previous buttons are not part of individual tabs,
                // we check their bounds independently of the tabIndex
                if (nextButton.Contains(e.Location) && SelectedIndex < TabCount - 1)
                {
                    SelectedIndex++;
                    NextButtonClick?.Invoke(this, new TabsDataEventarg { Tabidx = SelectedIndex });
                }
                else if (prevButton.Contains(e.Location) && SelectedIndex > 0)
                {
                    SelectedIndex--;
                    PrevButtonClick?.Invoke(this, new TabsDataEventarg { Tabidx = SelectedIndex });
                }
            }
          

            //// Next button click
            //if (nextButton.Contains(e.Location))
            //{
            //    if (this.SelectedIndex < this.TabCount - 1)
            //    {
            //        this.SelectedIndex++;
            //        NextButtonClick?.Invoke(this, new TabsDataEventarg() { Tabidx = this.SelectedIndex});
            //    }
            //}

            //// Previous button click
            //if (prevButton.Contains(e.Location))
            //{
            //    if (this.SelectedIndex > 0)
            //    {
            //        this.SelectedIndex--;
            //        PrevButtonClick?.Invoke(this, new TabsDataEventarg() { Tabidx = this.SelectedIndex });
            //    }
            //}
        }
        private void BeepTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.TabPages[e.Index];
            var tabRect = this.GetTabRect(e.Index);
            tabRect.Inflate(-2,-2);
            // Adjust the string drawing position and provide extra space for the close button
           // float textPadding = 0; // Space between the text and the close button
            float maxTextWidth = tabRect.Width - closeButtonImage.Width +10;
            e.Graphics.DrawString(tabPage.Text, tabPage.Font, Brushes.Black, new RectangleF(tabRect.Left, tabRect.Top, maxTextWidth, tabRect.Height));

           // Close button
            int closeButtonPadding = 4; // Padding from the right edge of the tab
            closeButton = new RectangleF(tabRect.Right - closeButtonImage.Width - closeButtonPadding, tabRect.Top + (tabRect.Height - closeButtonImage.Height) / 2, closeButtonImage.Width, closeButtonImage.Height);
            e.Graphics.DrawImage(closeButtonImage, closeButton.Location);
            // Update nextButton and prevButton rectangle definitions
            // Assume these buttons are drawn at the top-right corner of the tab control
            nextButton = new RectangleF(Width - nextButtonImage.Width - 20, 5, nextButtonImage.Width, nextButtonImage.Height);
            prevButton = new RectangleF(Width - nextButtonImage.Width - prevButtonImage.Width - 40, 5, prevButtonImage.Width, prevButtonImage.Height);

            // Next button
            if (e.Index == this.TabCount - 1)
            {
              //  nextButton = new RectangleF(this.Width - 20, tabRect.Top + 1, nextButtonImage.Width, nextButtonImage.Height);
                e.Graphics.DrawImage(nextButtonImage, nextButton.Location);
            }

            // Previous button
            if (e.Index == 0)
            {
               // prevButton = new RectangleF(this.Width - 40, tabRect.Top + 1, prevButtonImage.Width, prevButtonImage.Height);
                e.Graphics.DrawImage(prevButtonImage, prevButton.Location);
            }
            // Close button
            //closeButton = new RectangleF(tabRect.Right - 15, tabRect.Top , 12, 20);
            //e.Graphics.FillRectangle(Brushes.Red, closeButton);
            //e.Graphics.DrawString("X", tabPage.Font, Brushes.White, closeButton.Location);

            // Next button
            //if (e.Index == this.TabCount - 1)
            //{
            //    nextButton = new RectangleF(this.Width - 30, tabRect.Top, 12, 20);
            //    e.Graphics.FillRectangle(Brushes.Gray, nextButton);
            //    e.Graphics.DrawString(">", tabPage.Font, Brushes.White, nextButton.Location);
            //}

            //// Previous button
            //if (e.Index == 0)
            //{
            //    prevButton = new RectangleF(this.Width - 50, tabRect.Top, 12, 20);
            //    e.Graphics.FillRectangle(Brushes.Gray, prevButton);
            //    e.Graphics.DrawString("<", tabPage.Font, Brushes.White, prevButton.Location);
            //    //}
            //}
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_LBUTTONDOWN = 0x201;
            const int WM_LBUTTONUP = 0x202;

            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONUP)
            {
                // Convert the message's lParam (long parameter) to a Point
                Point clickPoint = new Point(m.LParam.ToInt32() & 0xFFFF, m.LParam.ToInt32() >> 16);

                // Convert the click point to the control's client area coordinate system
                clickPoint = PointToClient(clickPoint);

                if (nextButton.Contains(clickPoint))
                {
                    if (m.Msg == WM_LBUTTONUP) // Ensure the click is complete
                    {
                        OnNextButtonClick();
                        return; // Prevent further processing to avoid interfering with tab selection
                    }
                }
                else if (prevButton.Contains(clickPoint))
                {
                    if (m.Msg == WM_LBUTTONUP)
                    {
                        OnPrevButtonClick();
                        return;
                    }
                }
            }

            // Call the base class method to ensure standard message processing
            base.WndProc(ref m);
        }

        public void OnNextButtonClick()
        {
            if (SelectedIndex < TabCount - 1)
            {
                SelectedIndex++;
                NextButtonClick?.Invoke(this, new TabsDataEventarg { Tabidx = SelectedIndex });
            }
        }

        public void OnPrevButtonClick()
        {
            if (SelectedIndex > 0)
            {
                SelectedIndex--;
                PrevButtonClick?.Invoke(this, new TabsDataEventarg { Tabidx = SelectedIndex });
            }
        }

    }
}
