using Game.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Game : Base
    {
       
        private GameTime _gameTime;
        
        private Timer _timer;
       
        public event EventHandler<MouseEventArgs> Canvas_MouseUp;
        

        public Game()
        {
            InitializeComponent();
            _gameTime = new GameTime();
            Keyboard = new Keyboard();

            _timer = new Timer();
            _timer.Interval = 1000 / 30; 
            _timer.Tick += (sender, e) =>
            {
                var _now = DateTime.Now;
                _gameTime.FrameMilliseconds = (int)(_now - _gameTime.FrameDate).TotalMilliseconds;
                _gameTime.FrameDate = _now;

                Application.DoEvents();
                this.Update(_gameTime);  //запустить логику
                this.Keyboard.Clear();

                using (DrawHandler drawHandler = new DrawHandler(this.Canvas.Width, this.Canvas.Height))
                {
                    this.Draw(drawHandler);   //обновить изображение в каждом кадре
                    Canvas.Image = drawHandler.BaseImage; // присваиваем изображение нового блока картинному блоку
                }
            };

            _timer.Start(); // начало игры
        }
       
        
        protected Keyboard Keyboard { get; set; }
        

       
        private void pcCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (Canvas_MouseUp != null)
                Canvas_MouseUp(sender, e);
        }
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            this.Keyboard.SetKey(e.KeyData);
        }
       
        protected Image Load_Image(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show("Load File Error\n" + path);
                return null;
            }
        }
        
        protected  virtual void Update(GameTime gameTime)
        {
        }
        
        public virtual void Draw(DrawHandler drawHandler)
        {
        }


       

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
