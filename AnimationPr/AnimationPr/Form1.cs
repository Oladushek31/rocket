using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnimationPr
{
    public partial class Form1 : Form
    {
        private const int ShipWidth = 50; 
        private const int ShipHeight = 80; 
        private const int WindowWidth = 20; 
        private const int WindowHeight = 30; 
        private const int EngineWidth = 30;
        private const int EngineHeight = 20; 
        private const int StarCount = 100; 

        private Random random;
        private Point shipLocation;
        private int[] engineFlameSizes; 

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            random = new Random();
            shipLocation = new Point(this.ClientSize.Width / 2 - ShipWidth / 2, this.ClientSize.Height / 2 - ShipHeight / 2);

            engineFlameSizes = new int[5];
            for (int i = 0; i < engineFlameSizes.Length; i++)
            {
                engineFlameSizes[i] = random.Next(EngineHeight / 2, EngineHeight);
            }

            Timer timer = new Timer();
            timer.Interval = 50; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            shipLocation.X += random.Next(-2, 3); 
            shipLocation.Y += random.Next(-2, 3); 

            for (int i = 0; i < engineFlameSizes.Length; i++)
            {
                engineFlameSizes[i] = random.Next(EngineHeight / 2, EngineHeight);
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawShip(e.Graphics);

            for (int i = 0; i < StarCount; i++)
            {
                int x = random.Next(this.ClientSize.Width);
                int y = random.Next(this.ClientSize.Height);
                e.Graphics.FillRectangle(Brushes.White, new Rectangle(x, y, 2, 2)); 
            }
        }

        private void DrawShip(Graphics g)
        {
            g.FillRectangle(Brushes.DimGray, new Rectangle(shipLocation.X, shipLocation.Y, ShipWidth, ShipHeight));

            g.FillRectangle(Brushes.LightGray, new Rectangle(shipLocation.X + (ShipWidth - WindowWidth) / 2, shipLocation.Y + 10, WindowWidth, WindowHeight));

            int wingWidth = ShipWidth / 3; 
            int wingHeight = ShipHeight / 2; 

            int hatWidth = ShipWidth;
            int hatHeight = ShipWidth / 2;
            int hatX = shipLocation.X;
            int hatY = shipLocation.Y - hatHeight;

            g.FillPie(Brushes.Red, new Rectangle(hatX, hatY, hatWidth, hatHeight * 2), 180, 180);

            Point[] leftWingPoints =
            {
                new Point(shipLocation.X, shipLocation.Y + wingHeight),
                new Point(shipLocation.X - wingWidth, shipLocation.Y + ShipHeight),
                new Point(shipLocation.X, shipLocation.Y + ShipHeight)
            };
            g.FillPolygon(Brushes.Red, leftWingPoints);

            Point[] rightWingPoints =
            {
                new Point(shipLocation.X + ShipWidth, shipLocation.Y + wingHeight),
                new Point(shipLocation.X + ShipWidth + wingWidth, shipLocation.Y + ShipHeight),
                new Point(shipLocation.X + ShipWidth, shipLocation.Y + ShipHeight)
            };
            g.FillPolygon(Brushes.Red, rightWingPoints);

            int engineX = shipLocation.X + (ShipWidth - EngineWidth) / 2;
            int engineY = shipLocation.Y + ShipHeight;
            for (int i = 0; i < engineFlameSizes.Length; i++)
            {
                int flameHeight = engineFlameSizes[i];
                int flameWidth = EngineWidth - i * 5; 
                int flameX = engineX + (EngineWidth - flameWidth) / 2;
                int flameY = engineY + i * 10;
                Color flameColor = i % 3 == 0 ? Color.OrangeRed : i % 2 == 0 ? Color.Yellow : Color.Red; 
                g.FillRectangle(new SolidBrush(flameColor), new Rectangle(flameX, flameY, flameWidth, flameHeight));
            }
        }
    }
}
