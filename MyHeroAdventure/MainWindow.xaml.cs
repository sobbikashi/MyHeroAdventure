using System;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Security.Policy;

namespace MyHeroAdventure
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitialiseImage();
            ChangeImage(bitmapIdle);
            
        }
        public class Room
        {            
            
            public int BossPower { get; set; }
            public string RoomContents { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }

        public int heroPower = 25;
        public int roomCleared = 10;
        public int artifactsCount = 3;        
        public Room[] dungeon = new Room[10];
        Uri win = new Uri(@"E:\УЧЁБА\Repos\MyHeroAdventure\MyHeroAdventure\img\victory.jpg");
        Uri lose = new Uri(@"E:\УЧЁБА\Repos\MyHeroAdventure\MyHeroAdventure\img\defeat.png");
        Uri art = new Uri(@"E:\УЧЁБА\Repos\MyHeroAdventure\MyHeroAdventure\img\artifact.png");
        Uri shield = new Uri(@"E:\УЧЁБА\Repos\MyHeroAdventure\MyHeroAdventure\img\shield.png");


        BitmapImage bitmapArt = new BitmapImage();
        BitmapImage bitmapIdle= new BitmapImage();
        BitmapImage bitmapVictory = new BitmapImage();
        BitmapImage bitmapLoose = new BitmapImage();



        Random rnd = new Random(); 
        public List<int> artifactIndex = new List<int>();        
        public void InitialiseImage()
        {
            bitmapArt.BeginInit();
            bitmapArt.UriSource = art;
            bitmapArt.EndInit();
            bitmapLoose.BeginInit();
            bitmapLoose.UriSource = lose;
            bitmapLoose.EndInit();
            bitmapIdle.BeginInit();
            bitmapIdle.UriSource = shield;
            bitmapIdle.EndInit();
            bitmapVictory.BeginInit();
            bitmapVictory.UriSource = win;
            bitmapVictory.EndInit();           

        }

        public void ChangeImage(BitmapImage image)
        {
            imResult.Source = image;
            
        }
      


        #region Генератор имён боссов
        public string BossNameGenerator(int power)
        {
            string bossName = "Скелет";
            if ((power > 0) && (power <= 10))
            {
                bossName = "Здоровенная Бешеная Цыпа";
            }
            else if ((power > 10) && (power <= 20))
            {
                bossName = "3872 орка";
            }
            else if ((power > 20) && (power <= 30))
            {
                bossName = "Блуждающий Нос";
            }
            else if ((power > 30) && (power <= 40))
            {
                bossName = "Страховой Агент";
            }
            else if ((power > 40) && (power <= 50))
            {
                bossName = "Пикачу с Топором";
            }
            else if ((power > 50) && (power <= 60))
            {
                bossName = "Милый Кролик";
            }
            else if ((power > 60) && (power <= 70))
            {
                bossName = "Шовинистский Боров";
            }
            else if ((power > 70) && (power <= 80))
            {
                bossName = "Лососилиск";
            }
            else if ((power > 80) && (power <= 90))
            {
                bossName = "Челведведосвин";
            }
            else if ((power > 90) && (power <= 100))
            {
                bossName = "Газебо";
            }
            return bossName;
        }
        #endregion
        #region Генератор названий артефактов
        public string ArtifactNameGenerator(int power)
        {
            string artifactName = "Скелет";            
            
            if ((power >= 10) && (power <= 15)) artifactName = "Кольцо ВсеЗдрасти";
            if ((power > 15) && (power <= 20)) artifactName = "Vrblther's Vibroblade";
            if ((power > 20) && (power <= 25)) artifactName = "Демократизатор";
            if ((power > 25) && (power <= 30)) artifactName = "Розовые Очки";
            if ((power > 30) && (power <= 35)) artifactName = "Шашка Чапаева";
            if ((power > 35) && (power <= 40)) artifactName = "Синяя Изолента";
            if ((power > 40) && (power <= 45)) artifactName = "Бензопила Кровавого Расчленения";
            if ((power > 45) && (power <= 50)) artifactName = "Б.Ф.Г.";
            return artifactName;
        } 
        #endregion
        public void RoomSalvation()
        {

        }
        public void DungeonGenerator()
        {
            heroPower = 25;
            roomCleared = 10;
            //Генерируем адрес артефакта
            int artifactIndex = rnd.Next(0, 11);          
            
            //Генерируем подземелье из боссов и артефактов
            for (int i = 0; i < 10; i++)
            {
                    dungeon[i] = new Room();
                    if (i == artifactIndex)
                    {
                        dungeon[i].RoomContents = "Artifact";
                        dungeon[i].BossPower = rnd.Next(10, 81);
                        dungeon[i].Name = ArtifactNameGenerator(dungeon[i].BossPower);
                    }
                    else
                    {
                        dungeon[i].RoomContents = "Boss";
                        dungeon[i].BossPower = rnd.Next(5, 100);
                        dungeon[i].Name = BossNameGenerator(dungeon[i].BossPower);
                    }                   
                    dungeon[i].IsChecked = false;                   
                    
            }          
            
           

            lbPower.Content = heroPower.ToString();
            lbIndicator.Content = "ВЫБЕРИ СВОЙ ПУТЬ!";
            ChangeImage(bitmapIdle);

        }
        public void RoomCheck(int roomNumber)
        {
            if (dungeon[roomNumber-1].IsChecked == false)
            {
                if (dungeon[roomNumber - 1].RoomContents == "Boss")
                {
                    if (heroPower >= dungeon[roomNumber - 1].BossPower)
                    {
                        lbIndicator.Content = "ПОБЕДА!!!";
                        roomCleared = --roomCleared;
                        heroPower = heroPower + 5;
                        lbPower.Content = heroPower.ToString();
                        tbStatus.Text = "На Вас напал монстр " + dungeon[roomNumber - 1].Name + " силой " + dungeon[roomNumber-1].BossPower + "\n" + "За победу Вы получаете 5 силы";
                        ChangeImage(bitmapVictory);
                    }
                    else
                    {
                        ChangeImage(bitmapLoose);
                        lbIndicator.Content = "ПОРАЖЕНИЕ!";
                        MessageBox.Show("Поражение");
                        DungeonGenerator();                        
                    }
                }
                else
                {
                    if ((dungeon[roomNumber-1].BossPower >= 50) | (dungeon[roomNumber - 1].BossPower < 10))
                    {
                        dungeon[roomNumber - 1].BossPower = rnd.Next(10, 51);
                    }
                   tbStatus.Text = "Вы нашли артефакт " + dungeon[roomNumber-1].Name.ToString() + " силой " + dungeon[roomNumber - 1].BossPower.ToString();
                   heroPower = heroPower + dungeon[roomNumber - 1].BossPower;
                   lbPower.Content = heroPower.ToString(); 
                   roomCleared = --roomCleared;    
                   ChangeImage(bitmapArt);
                }

                dungeon[roomNumber - 1].IsChecked = true;
                
            }
            else
            {
                tbStatus.Text = "КОМНАТА ПУСТА!";
            }
            lbRoomClearCount.Content = roomCleared;
            if (roomCleared == 0)
            {
                MessageBox.Show("ВЫ ПОБЕДИЛИ!!!!");
                DungeonGenerator();
            }

        }

        #region Генерация данных
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            DungeonGenerator();
        } 
        #endregion

        #region Проверка сгенерированных данных
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            tbTest.Text = "";
            for (int i = 0; i < 10; i++)
            {
                tbTest.Text = tbTest.Text + dungeon[i].BossPower + " " + dungeon[i].RoomContents + " " + dungeon[i].IsChecked.ToString() + dungeon[i].Name + "\n";
            }
        }
        #endregion

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(1);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(2);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(3);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(4);
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(5);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(6);
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(7);
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(8);
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(9);
        }

        private void btn10_Click(object sender, RoutedEventArgs e)
        {
            RoomCheck(10);
        }

        private void tbCommand_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbCommand.Text == "Пройти")
            {
                MessageBox.Show("Хорошо");
            }
        }
    }
    
}
