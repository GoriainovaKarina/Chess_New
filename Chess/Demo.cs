using Chess.Elements;
using Game;
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

namespace Chess
{
   
    public partial class Demo : Game.Game
    {

        public Demo() : base()
        {
            InitializeComponent();

            Initialize();
            Start_Match();
            Start_Match1();
            Start_Match2();
        }
        
       
        public Resources Resources { get; set; }
       
        private Board Board { get; set; }
       
        private List<Piece_Base> Pieces { get; set; }
       
        public Player Player1 { get; set; }
       
        public Player Player2 { get; set; }
        
        public Player CurrentPlayer { get; set; }
        
        public List<ActionLog> ActionLog { get; set; }
       
        public State GameState { get; set; }
       

       
        private void btnStart_Click(object sender, EventArgs e)
        {
            Start_Match();
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            var lastAction = this.ActionLog.LastOrDefault();
            if (lastAction != null)
            {
                lastAction.Moves.ForEach(m => m.Piece.Location = m.Original_Location);
                lastAction.Removed_Pieces.ForEach(x => this.Pieces.Add(x));

                if (lastAction.Added_Piece != null)
                    this.Pieces.Remove(lastAction.Added_Piece);

                this.ActionLog.Remove(lastAction);
                Next_Turn(false);
                Board.Clear_EnabledMoves();
                this.Pieces.ForEach(p => p.Selected = false);
            }
        }
        private void Demo_Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Point _mouseLocation = new Point(e.Location.X - 5, e.Location.Y - 5); //Остальные края доски
            var cell_Location = new Point(e.Location.X / 105, e.Location.Y / 105); // каждая ячейка имеет размер границы 100x100 + 5x5
            // получаю координаты доски, где я нажал

            if (!Move_Piece(cell_Location)) // если есть выделенная часть, попробуйте переместить ее в ячейку, в которой она была сделана. нажмите
                Set_SelectedPiece(cell_Location); // если выбранная фигура не может быть перемещена в ячейку назначения, делается попытка выбрать другую фигуру
        }
      
       
        private void Initialize()
        {
            string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            this.Resources = new Resources()
            {
                Image_BoardTiles = Load_Image($"{directory}/Board.png"),
                Image_MoveTiles = Load_Image($"{directory}/TileMove.png"),
                Image_SelectedTile = Load_Image($"{directory}/TileSelected.png"),
                Image_WhitePawn = Load_Image($"{directory}/White_Pawn.png"),
                Image_White_Rook = Load_Image($"{directory}/White_Rook.png"),
                Image_White_Knight = Load_Image($"{directory}/White_Knight.png"),
                Image_White_Bishop = Load_Image($"{directory}/White_Bishop.png"),
                Image_White_Queen = Load_Image($"{directory}/White_Queen.png"),
                Image_White_King = Load_Image($"{directory}/White_King.png"),
                Image_BlackPawn = Load_Image($"{directory}/Black_Pawn.png"),
                Image_Black_Rook = Load_Image($"{directory}/Black_Rook.png"),
                Image_Black_Knight = Load_Image($"{directory}/Black_Knight.png"),
                Image_Black_Bishop = Load_Image($"{directory}/Black_Bishop.png"),
                Image_Black_Queen = Load_Image($"{directory}/Black_Queen.png"),
                Image_Black_King = Load_Image($"{directory}/Black_King.png")
            };
        }
       
        private void Start_Match()
        {
            this.ActionLog = new List<ActionLog>();
            this.Pieces = new List<Piece_Base>();
            this.Board = new Board(this.Resources.Image_BoardTiles, this.Resources.Image_MoveTiles);
            this.GameState = State.Normal;

            //белые
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Rook(this.Resources.Image_White_Rook, Piece_Color.White));
            Add_Piece(new Piece_Knight(this.Resources.Image_White_Knight, Piece_Color.White));
            Add_Piece(new Piece_Bishop(this.Resources.Image_White_Bishop, Piece_Color.White));
            Add_Piece(new Piece_Queen(this.Resources.Image_White_Queen, Piece_Color.White));
            Add_Piece(new Piece_King(this.Resources.Image_White_King, Piece_Color.White));
            Add_Piece(new Piece_Bishop(this.Resources.Image_White_Bishop, Piece_Color.White));
            Add_Piece(new Piece_Knight(this.Resources.Image_White_Knight, Piece_Color.White));
            Add_Piece(new Piece_Rook(this.Resources.Image_White_Rook, Piece_Color.White));
            //чёрные
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Rook(this.Resources.Image_Black_Rook, Piece_Color.Black));
            Add_Piece(new Piece_Knight(this.Resources.Image_Black_Knight, Piece_Color.Black));
            Add_Piece(new Piece_Bishop(this.Resources.Image_Black_Bishop, Piece_Color.Black));
            Add_Piece(new Piece_King(this.Resources.Image_Black_King, Piece_Color.Black));
            Add_Piece(new Piece_Queen(this.Resources.Image_Black_Queen, Piece_Color.Black));
            Add_Piece(new Piece_Bishop(this.Resources.Image_Black_Bishop, Piece_Color.Black));
            Add_Piece(new Piece_Knight(this.Resources.Image_Black_Knight, Piece_Color.Black));
            Add_Piece(new Piece_Rook(this.Resources.Image_Black_Rook, Piece_Color.Black));

            Player1 = new Player(rbBlack.Checked ? Piece_Color.Black : Piece_Color.White, Player_Type.Human, 1);
            Player2 = new Player(rbBlack.Checked ? Piece_Color.White : Piece_Color.Black, Player_Type.Human, 2);
            CurrentPlayer = Player1.Color == Piece_Color.White ? Player1 : Player2; //  игрок, который использует белые фигуры, начинает игру

            // координаты фишек первого игрока
            var lstPiecesPlayer1 = this.Pieces.Where(x => x.Color == Player1.Color).ToList();
            lstPiecesPlayer1[0].Location = new Point(0, 6);
            lstPiecesPlayer1[1].Location = new Point(1, 6);
            lstPiecesPlayer1[2].Location = new Point(2, 6);
            lstPiecesPlayer1[3].Location = new Point(3, 6);
            lstPiecesPlayer1[4].Location = new Point(4, 6);
            lstPiecesPlayer1[5].Location = new Point(5, 6);
            lstPiecesPlayer1[6].Location = new Point(6, 6);
            lstPiecesPlayer1[7].Location = new Point(7, 6);
            lstPiecesPlayer1[8].Location = new Point(0, 7);
            lstPiecesPlayer1[9].Location = new Point(1, 7);
            lstPiecesPlayer1[10].Location = new Point(2, 7);
            lstPiecesPlayer1[11].Location = new Point(3, 7);
            lstPiecesPlayer1[12].Location = new Point(4, 7);
            lstPiecesPlayer1[13].Location = new Point(5, 7);
            lstPiecesPlayer1[14].Location = new Point(6, 7);
            lstPiecesPlayer1[15].Location = new Point(7, 7);

            //координаты фишек второго игрока
            var lstPiecesPlayer2 = this.Pieces.Where(x => x.Color == Player2.Color).ToList();
            lstPiecesPlayer2[0].Location = new Point(7, 1);
            lstPiecesPlayer2[1].Location = new Point(6, 1);
            lstPiecesPlayer2[2].Location = new Point(5, 1);
            lstPiecesPlayer2[3].Location = new Point(4, 1);
            lstPiecesPlayer2[4].Location = new Point(3, 1);
            lstPiecesPlayer2[5].Location = new Point(2, 1);
            lstPiecesPlayer2[6].Location = new Point(1, 1);
            lstPiecesPlayer2[7].Location = new Point(0, 1);
            lstPiecesPlayer2[8].Location = new Point(7, 0);
            lstPiecesPlayer2[9].Location = new Point(6, 0);
            lstPiecesPlayer2[10].Location = new Point(5, 0);
            lstPiecesPlayer2[11].Location = new Point(4, 0);
            lstPiecesPlayer2[12].Location = new Point(3, 0);
            lstPiecesPlayer2[13].Location = new Point(2, 0);
            lstPiecesPlayer2[14].Location = new Point(1, 0);
            lstPiecesPlayer2[15].Location = new Point(0, 0);

            Next_Turn(true);
            base.Enabled = true;
        }







        private void Start_Match1()
        {

            this.ActionLog = new List<ActionLog>();
            this.Pieces = new List<Piece_Base>();
            this.Board = new Board(this.Resources.Image_BoardTiles, this.Resources.Image_MoveTiles);
             this.GameState = State.Normal;

            //белые
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
             Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
             Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
             Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
             Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
             Add_Piece(new Piece_Rook(this.Resources.Image_White_Rook, Piece_Color.White));
             Add_Piece(new Piece_Knight(this.Resources.Image_White_Knight, Piece_Color.White));
             Add_Piece(new Piece_Bishop(this.Resources.Image_White_Bishop, Piece_Color.White));
            Add_Piece(new Piece_Queen(this.Resources.Image_White_Queen, Piece_Color.White));
             Add_Piece(new Piece_King(this.Resources.Image_White_King, Piece_Color.White));
            Add_Piece(new Piece_Bishop(this.Resources.Image_White_Bishop, Piece_Color.White));
             Add_Piece(new Piece_Knight(this.Resources.Image_White_Knight, Piece_Color.White));
            Add_Piece(new Piece_Rook(this.Resources.Image_White_Rook, Piece_Color.White));
            //чёрные
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
             Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
             Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
             Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
             Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
             Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
             Add_Piece(new Piece_Rook(this.Resources.Image_Black_Rook, Piece_Color.Black));
            Add_Piece(new Piece_Knight(this.Resources.Image_Black_Knight, Piece_Color.Black));
             Add_Piece(new Piece_Bishop(this.Resources.Image_Black_Bishop, Piece_Color.Black));
             Add_Piece(new Piece_King(this.Resources.Image_Black_King, Piece_Color.Black));
             Add_Piece(new Piece_Queen(this.Resources.Image_Black_Queen, Piece_Color.Black));
            Add_Piece(new Piece_Bishop(this.Resources.Image_Black_Bishop, Piece_Color.Black));
            Add_Piece(new Piece_Knight(this.Resources.Image_Black_Knight, Piece_Color.Black));
            Add_Piece(new Piece_Rook(this.Resources.Image_Black_Rook, Piece_Color.Black));

             Player1 = new Player(rbBlack.Checked ? Piece_Color.Black : Piece_Color.White, Player_Type.Human, 1);
             Player2 = new Player(rbBlack.Checked ? Piece_Color.White : Piece_Color.Black, Player_Type.Human, 2);
            CurrentPlayer = Player1.Color == Piece_Color.White ? Player1 : Player2; //  игрок, который использует белые фигуры, начинает игру


            var lstPiecesPlayer1 = this.Pieces.Where(x => x.Color == Player1.Color).ToList();
           var lstPiecesPlayer2 = this.Pieces.Where(x => x.Color == Player2.Color).ToList();
            

              lstPiecesPlayer1[11].Location = new Point(6, 3);
              lstPiecesPlayer1[12].Location = new Point(5, 1);
              lstPiecesPlayer2[0].Location = new Point(7, 1);
              lstPiecesPlayer2[1].Location = new Point(6, 1);
             lstPiecesPlayer2[11].Location = new Point(7, 0);
             


              Next_Turn(true);
              base.Enabled = true;
           
        }


        private void Start_Match2()
        {

            this.ActionLog = new List<ActionLog>();
            this.Pieces = new List<Piece_Base>();
            this.Board = new Board(this.Resources.Image_BoardTiles, this.Resources.Image_MoveTiles);
            this.GameState = State.Normal;

            //белые
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Pawn(this.Resources.Image_WhitePawn, Piece_Color.White));
            Add_Piece(new Piece_Rook(this.Resources.Image_White_Rook, Piece_Color.White));
            Add_Piece(new Piece_Knight(this.Resources.Image_White_Knight, Piece_Color.White));
            Add_Piece(new Piece_Bishop(this.Resources.Image_White_Bishop, Piece_Color.White));
            Add_Piece(new Piece_Queen(this.Resources.Image_White_Queen, Piece_Color.White));
            Add_Piece(new Piece_King(this.Resources.Image_White_King, Piece_Color.White));
            Add_Piece(new Piece_Bishop(this.Resources.Image_White_Bishop, Piece_Color.White));
            Add_Piece(new Piece_Knight(this.Resources.Image_White_Knight, Piece_Color.White));
            Add_Piece(new Piece_Rook(this.Resources.Image_White_Rook, Piece_Color.White));
            //чёрные
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Pawn(this.Resources.Image_BlackPawn, Piece_Color.Black));
            Add_Piece(new Piece_Rook(this.Resources.Image_Black_Rook, Piece_Color.Black));
            Add_Piece(new Piece_Knight(this.Resources.Image_Black_Knight, Piece_Color.Black));
            Add_Piece(new Piece_Bishop(this.Resources.Image_Black_Bishop, Piece_Color.Black));
            Add_Piece(new Piece_King(this.Resources.Image_Black_King, Piece_Color.Black));
            Add_Piece(new Piece_Queen(this.Resources.Image_Black_Queen, Piece_Color.Black));
            Add_Piece(new Piece_Bishop(this.Resources.Image_Black_Bishop, Piece_Color.Black));
            Add_Piece(new Piece_Knight(this.Resources.Image_Black_Knight, Piece_Color.Black));
            Add_Piece(new Piece_Rook(this.Resources.Image_Black_Rook, Piece_Color.Black));

            Player1 = new Player(rbBlack.Checked ? Piece_Color.Black : Piece_Color.White, Player_Type.Human, 1);
            Player2 = new Player(rbBlack.Checked ? Piece_Color.White : Piece_Color.Black, Player_Type.Human, 2);
            CurrentPlayer = Player1.Color == Piece_Color.White ? Player1 : Player2; //  игрок, который использует белые фигуры, начинает игру


            var lstPiecesPlayer1 = this.Pieces.Where(x => x.Color == Player1.Color).ToList();
            var lstPiecesPlayer2 = this.Pieces.Where(x => x.Color == Player2.Color).ToList();
           
                lstPiecesPlayer1[13].Location = new Point(7, 6);
                lstPiecesPlayer1[15].Location = new Point(3, 5);
                lstPiecesPlayer1[14].Location = new Point(2, 5);
                lstPiecesPlayer1[12].Location = new Point(1, 4);
                lstPiecesPlayer2[11].Location = new Point(2, 3);
                lstPiecesPlayer2[10].Location = new Point(4, 2);
                lstPiecesPlayer2[10].Location = new Point(0, 1);
            

            Next_Turn(true);
            base.Enabled = true;
           
        }

        private Point Get_PiecePosition(Point location)
        {
            int _x = (location.X * 100) + 5 * (location.X + 1);
            int _y = (location.Y * 100) + 5 * (location.Y + 1);
            return new Point(_x, _y);
        }
      
        private void Add_Piece(Piece_Base piece, ActionLog log = null)
        {
            piece.SelectedImage = this.Resources.Image_SelectedTile; // назначает изображение для отображения, если выбрано
            this.Pieces.Add(piece);  
            if (log != null)
                log.Added_Piece = piece;
        }
       
        private void Remove_Piece(Piece_Base piece, ActionLog log)
        {
            this.Pieces.Remove(piece);  //Удалить фигуру из списка фигур на доске
            log.Removed_Pieces.Add(piece);
        }
       
        private void Next_Turn(bool firstTurn)
        {
            this.GameState = State.Normal;
            lblCheck.Text = string.Empty;
            lblMoves.Text = string.Empty;

            if (!firstTurn)
                CurrentPlayer = CurrentPlayer.Number == 1 ? Player2 : Player1;
            Set_MovesLocations(); //  пересчитывает движения для каждой фигуры

            int moves = this.Pieces.Where(x => x.Color == CurrentPlayer.Color).Sum(x => x.EnabledMoves.Count());
            lblMoves.Text = moves.ToString();

            var king = this.Pieces.FirstOrDefault(x => x.Color == CurrentPlayer.Color && x.GetType() == typeof(Piece_King));
            var isCheck = this.Pieces.Any(x => x.Color != CurrentPlayer.Color && x.EnabledMoves.Contains(king.Location));
            // проверяет, остался ли король в чеке в предыдущей игре
            if (isCheck)
            {
                this.GameState = State.Check;
                if (moves == 0) // если у нас кончатся движения, это мат
                    this.GameState = State.Checkmate;
            }
            else
            {
                if (moves == 0) // если у нас закончились ходы и это не проверка, игра на столе
                    this.GameState = State.Draw;
            }

            lblCheck.Text = this.GameState.ToString();
            if (GameState == State.Checkmate || GameState == State.Draw)
            {
                MessageBox.Show(this.GameState.ToString(), "Chess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void Set_SelectedPiece(Point cell_Location)
        {
            Board.Clear_EnabledMoves();
            this.Pieces.ForEach(x => x.Selected = false); // отменяю все фишки
            Piece_Base _selectedPiece = this.Pieces.FirstOrDefault(x => x.Location == cell_Location && x.Color == CurrentPlayer.Color);
            //ищу ячейку для координаты, где она была нажата, только если это цвет, соответствующий игроку, который имеет ход

            if (_selectedPiece != null)
            {
                _selectedPiece.Selected = true; // ячейка выбрана
                Array.ForEach(_selectedPiece.EnabledMoves, loc => Board.Cells[loc.X, loc.Y].CanMove = true); // ячейки с включенным цветом
            }
        }

       
        private void Set_MovesLocations()
        {
            this.Pieces.ForEach(x => x.EnabledMoves = Get_MovesLocations(x, this.Pieces)); //разрешённые движения, которые есть у фигуры

            // проверяет доступные ходы, которые может выполнить текущий игрок
            this.Pieces.Where(x => x.Color == CurrentPlayer.Color).ToList().ForEach(p =>
            {
                p.EnabledMoves = p.EnabledMoves.Where(loc => Valid_MovesLocations_Check(p, loc)).ToArray();
                // подтверждает, что король не находится под прицелом, когда делает ход
            });
        }
        
        private Point[] Get_MovesLocations(Piece_Base piece, List<Piece_Base> boardPieces)
        {
            List<Point> lstAvailableCell = new List<Point>();
            if (piece != null)
            {
                Array.ForEach(piece.Moves, x =>
                {
                    var displacement = x.Direction;
                    if (piece.Color == Player2.Color)
                    {
                        displacement = new Point(displacement.X * -1, displacement.Y * -1);
                        
                    }

                    Point _location = piece.Location; // начальная позиция, с которой начинается проверка ячеек
                    while (true)
                    {
                        _location = new Point(_location.X + displacement.X, _location.Y + displacement.Y);
                        //положение ячейки для проверки

                        if (_location.X > 7 || _location.Y > 7 || _location.X < 0 || _location.Y < 0)
                            break; // координировать с доски "ДВИЖЕНИЕ НЕ ДОПУСКАЕТСЯ"

                        var _targetPiece = boardPieces.FirstOrDefault(y => y.Location == _location);

                        if (x.Type.HasFlag(Elements.Move_Type.Special)) // специальное движение

                        {
                            if (!Get_MovesLocations_Special(piece, x, _targetPiece))
                                break; // "НЕ РАЗРЕШАЕТ ДВИЖЕНИЯ"
                        }
                        else
                        {
                            if (_targetPiece != null && _targetPiece.Color == piece.Color) // 
                                break; //целевая фигура того же цвета, что и выбранная фигура "НЕ ДОПУСКАЕТ ДВИЖЕНИЯ"
                        }

                        lstAvailableCell.Add(_location); // добавляет координату в список включенных ячеек "ПОЗВОЛЯЕТ ДВИЖЕНИЕ"

                        if (_targetPiece != null && _targetPiece.Color != piece.Color)
                            break; // не допускает последующие перемещения в положение соперника

                        if (!x.IsLinear) 
                            break;
                    }
                });
            }

            return lstAvailableCell.ToArray();
        }
       
        private bool Valid_MovesLocations_Check(Piece_Base piece, Point newLocation)
        {
            Point _originalLocation = piece.Location; // текущая позиция фигуры
            piece.Location = newLocation; //назначает фигуре новую позицию для анализа, если король находится под прицелом

            bool _result = true;

            var lstPieces = this.Pieces.Where(x =>
                x.Color != piece.Color &&
                x.Location != newLocation).ToList();// получить конкурирующие фигуры, чтобы увидеть, есть ли какой - либо мат

            if (lstPieces.Any()) // список соперников, которые атакуют выбранную фигуру
            {
                var king = this.Pieces.First(x => x.Color == piece.Color && x.GetType() == typeof(Piece_King));

               
                                var lstBoardPieces = this.Pieces.Where(x => !(x.Color != piece.Color && x.Location == newLocation)).ToList(); 
                foreach (var p in lstPieces)
                {
                    var lstMoves = Get_MovesLocations(p, lstBoardPieces); // получить места смещения каждой фигуры противника
                    if (lstMoves.Any(x => x == king.Location))
                    {
                        _result = false; // если хотя бы одна фигура соперника остается в пределах досягаемости короля, движение считается недействительным
                        break;
                    }
                }
            }

            piece.Location = _originalLocation; //  назначаю оригинальное местоположение части
            return _result;
        }
       
        private bool Get_MovesLocations_Special(Piece_Base piece, Piece_Move move, Piece_Base rivalPiece)
        {
            // ИЗМЕНИТЬ ЛОГИКУ
            // возвращаем Tru при каждой проверке

            if (piece.GetType() == typeof(Piece_Pawn))
            {
                
                if (move.Direction.X == 0 && move.Direction.Y == -1) // ячейка смещения на одну вперёд
                    return rivalPiece == null; // ячейка спереди должна быть пустой

                if (move.Direction.X == 0 && move.Direction.Y == -2) // переднее смещение 2 ячеек не позволяет отаковать 
                {
                    bool _condicion1 = rivalPiece == null; // ячейка назначения должна быть пустой
                    bool _condicion2 = !this.Pieces.Any(p => p.Location.X == piece.Location.X && p.Location.Y == (CurrentPlayer.Number == 1 ? 5 : 2));
                    // передняя ячейка должна быть пустой
                    bool _condicion3 = (CurrentPlayer.Number == 1 && piece.Location.Y == 6) || (CurrentPlayer.Number == 2 && piece.Location.Y == 1);
                    // пешка должна ходить первой

                    return _condicion1 && _condicion2 && _condicion3;
                }

                if ((move.Direction.X == -1 || move.Direction.X == 1) && move.Direction.Y == -1)
                // атаковать соперника по диагонали
                {
                    var lastAction = ActionLog.LastOrDefault();
                    if (rivalPiece == null && lastAction != null)
                    // ЕСТЬ ПО ШАГУ
                    {
                        var Last_Move = lastAction.Moves.Last();
                        // Противостоящая пешка должна находиться в той же строке, что и перемещаемая фигура, в соседнем столбце и должна была переместиться в последний ход сыграло 2 ячейки
                        if (CurrentPlayer.Number == 1 && piece.Location.Y == 3)
                        // пешка движется вверх
                        {
                            var rival = this.Pieces.FirstOrDefault(p => p.Color != piece.Color && p.Location == new Point(piece.Location.X + move.Direction.X, piece.Location.Y));
                            if (rival != null && Last_Move.Piece.Equals(rival) && (Last_Move.New_Location.Y - Last_Move.Original_Location.Y) == 2)
                                return true;
                            // Включить еду на ходу
                        }
                        if (CurrentPlayer.Number == 2 && piece.Location.Y == 4)
                        // пешка движется вниз
                        {
                            var rival = this.Pieces.FirstOrDefault(p => p.Color != piece.Color && p.Location == new Point(piece.Location.X - move.Direction.X, piece.Location.Y));
                            if (rival != null && Last_Move.Piece.Equals(rival) && (Last_Move.New_Location.Y - Last_Move.Original_Location.Y) == -2)
                                return true;
                            // Включить еду на ходу
                        }
                    }

                    if (rivalPiece != null && rivalPiece.Color != piece.Color)
                        return true;
                    // движение разрешено, только если в целевой позиции есть противник
                }
               
            }
            else if (piece.GetType() == typeof(Piece_King))
            {
                if (GameState != State.Normal)
                    // король не может быть под прицелом
                    return false;

                
                bool kingFirstMove = !ActionLog.Any(x => x.Moves.Any(y => y.Piece.Equals(piece)));
                
                if (!kingFirstMove)
                    return false;

                Point _moveDirection = CurrentPlayer.Number == 1 ? move.Direction : new Point(move.Direction.X * -1, move.Direction.Y * -1);
                Piece_Base _rock = null;
                if (_moveDirection.X < 0) 
                    _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Piece_Rook) && p.Location.X == 0 && p.Location.Y == piece.Location.Y);
                else 
                    _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Piece_Rook) && p.Location.X == 7 && p.Location.Y == piece.Location.Y);
                if (_rock == null) // если башня не существует, предполагается, что она была удалена или перемещена из своего первоначальной ячейки
                    return false;

                bool rookFirstMove = !ActionLog.Any(x => x.Moves.Any(y => y.Piece.Equals(piece))); 
                if (!rookFirstMove) 
                    return false;

                int _moveX = _moveDirection.X < 0 ? -1 : 1;
                Point _location = new Point(piece.Location.X + _moveX, piece.Location.Y);
                while (_location != _rock.Location)
                {
                    bool _existPiece = this.Pieces.Any(p => p.Location == _location);
                    if (_existPiece)
                        return false;
                    // между королем и башней не должно быть места

                    bool _attackLoc = this.Pieces.Any(p => p.Color != piece.Color && p.EnabledMoves != null && p.EnabledMoves.Any(y => y == _location));
                    if (_attackLoc)
                        return false; // локация не может быть атакована соперником

                    _location = new Point(_location.X + _moveX, _location.Y);
                }

                return true;
                // рокировка действительна
            }

            return false;
        }

       
        private bool Move_Piece(Point cell_Location)
        {
            if (cell_Location.X > 7 || cell_Location.Y > 7 || cell_Location.X < 0 || cell_Location.Y < 0)
                return false; // координаты с доски "НЕ ДОПУСКАЕТ ДВИЖЕНИЯ"

            var selectedPiece = this.Pieces.FirstOrDefault(x => x.Selected);
            if (selectedPiece != null)
            {
                if (Board.Cells[cell_Location.X, cell_Location.Y].CanMove)
                // определить, может ли выбранная фигура двигаться по указанной координате
                {
                    var actionLog = new ActionLog();
                    actionLog.Moves.Add(new MoveLog
                    {
                        Piece = selectedPiece,
                        Original_Location = selectedPiece.Location,
                        New_Location = cell_Location
                    });
                    this.ActionLog.Add(actionLog);
                   

                    Move_Piece_Special(selectedPiece, cell_Location, actionLog);
                   

                    var targetPiece = this.Pieces.FirstOrDefault(x => x.Color != selectedPiece.Color && x.Location == cell_Location);
                    if (targetPiece != null)
                        Remove_Piece(targetPiece, actionLog); // удалить ячейку, которая находится в целевой позиции

                    selectedPiece.Location = cell_Location; //  перемещает выбранную фигуру к целевой координате
                    selectedPiece.Selected = false;
                    Board.Clear_EnabledMoves();

                    

                    Next_Turn(false);
                    // После перемещения фигуры начинается ход другого игрока
                    return true;
                }
            }

            return false;
        }
      
        private void Move_Piece_Special(Piece_Base piece, Point targetLocation, ActionLog log)
        {
            if (piece.GetType() == typeof(Piece_Pawn))
            {
               
                if (targetLocation.Y == (piece.Color == Player1.Color ? 0 : 7)) // игрок один коронует фигуру в верхнем ряду и игрок 2 в нижнем ряду
                {
                    Piece_Base _newPiece = null;
                    if (CurrentPlayer.Type == Player_Type.Human)
                    {
                        while (_newPiece == null)
                        {
                            var form = new PieceSelector(this.Resources, piece.Color);
                            form.ShowDialog();
                            _newPiece = form.Selected_Piece;
                        }
                        _newPiece.Location = targetLocation;
                    }

                    Remove_Piece(piece, log); // Ликвидировать пешку
                    Add_Piece(_newPiece, log); // добавить новую фигуру
                }

                
                var displacement = new Point(targetLocation.X - piece.Location.X, targetLocation.Y - piece.Location.Y);
                if ((displacement.X == -1 || displacement.X == 1) && displacement.Y == (CurrentPlayer.Equals(Player1) ? -1 : 1) && !this.Pieces.Any(x => x.Location == targetLocation))
                {
                    Point rivalLocation = new Point(targetLocation.X, targetLocation.Y + (CurrentPlayer.Equals(Player1) ? 1 : -1));
                    var romovePiece = this.Pieces.First(p => p.Location == rivalLocation);
                    Remove_Piece(romovePiece, log); // удалить пешку соперника
                }
               
            }
            if (piece.GetType() == typeof(Piece_King))
            {
                if (Math.Abs(piece.Location.X - targetLocation.X) == 2) // рокировка
                {
                    Piece_Base _rock = null;
                    if ((targetLocation.X - piece.Location.X) < 0) // 
                        _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Piece_Rook) && p.Location.X == 0 && p.Location.Y == piece.Location.Y);
                    else // 
                        _rock = this.Pieces.FirstOrDefault(p => p.GetType() == typeof(Piece_Rook) && p.Location.X == 7 && p.Location.Y == piece.Location.Y);

                    var _newRookLoc = _rock.Location.X == 7 ?
                        new Point(_rock.Location.X - 2, _rock.Location.Y) :
                        new Point(_rock.Location.X + 3, _rock.Location.Y);

                    log.Moves.Add(new MoveLog()
                    {
                        Piece = _rock,
                        Original_Location = _rock.Location,
                        New_Location = _newRookLoc
                    });

                    _rock.Location = _newRookLoc;
                }
            }
        }
       
        protected override void Update(GameTime gameTime)
        {
            this.Pieces.ForEach(x => x.Position = Get_PiecePosition(x.Location));
            // Обновляет положение экрана каждого элемента в соответствии с его координатами на доске.

            lblPlayer.Text = $"Player { (CurrentPlayer.Equals(Player1) ? "1" : "2") }";

        }
       
        public override void Draw(DrawHandler drawHandler)
        {
            this.Board.Draw(drawHandler);
            this.Pieces.ForEach(x => x.Draw(drawHandler));
        }
       

        private void Canvas_Click(object sender, EventArgs e)
        {

        }

        private void Demo_Load(object sender, EventArgs e)
        {

        }
        int clickCount = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            ++clickCount;
            switch(clickCount)
            {
                case 1 : Start_Match1();
                    break;
                case 2: Start_Match2();
                    break;
            }
            Start_Match1();
        }
    }
    public enum State
    {
        Normal,
        Check,
        Checkmate,
        Draw
    }
}

