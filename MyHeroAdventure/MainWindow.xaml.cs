using System;
using System.Collections.Generic;
using System.Windows;

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
        }
        public class Room
        {
            public int BossPower { get; set; }
            public string RoomContents { get; set; }
            public bool IsChecked { get; set; }
        }

        public int heroPower = 25;
        public int roomCleared = 0;
        public int artifactsCount = 1;
        public Room[] dungeon = new Room[10];
        
        Random rnd = new Random(); 
        public List<int> artifactIndex = new List<int>();
        public void DungeonGenerator()
        {
            //dungeon = new Room[10];
            //Генерируем количество артефактов
            int artifactsCount = rnd.Next(1, 5);
            //Генерируем индексы артефактов в массиве дверей
            for (int i = 0; i < artifactsCount+1; i++)
            {
                artifactIndex.Add(rnd.Next(1, 10));
            }
            


            //Генерируем подземелье из боссов и артефактов
            for (int i = 0; i < 10; i++)
            {
                dungeon[i] = new Room();
                for (int j = 0; j < artifactIndex.Count; j++)
                {
                    if (i == artifactIndex[j])
                    {
                        dungeon[i].RoomContents = "Artifact";
                    }
                    else
                    {
                        dungeon[i].RoomContents = "Boss";
                    }
                    if (dungeon[i].RoomContents == "Artifact")
                    {
                        dungeon[i].BossPower = rnd.Next(10,81);
                    } else
                    {
                        dungeon[i].BossPower = rnd.Next(5, 100);
                    }                    
                    
                }
            }

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
                        roomCleared = ++roomCleared;
                        
                    }
                    else
                    {
                        lbIndicator.Content = "ПОРАЖЕНИЕ!";
                        MessageBox.Show("Поражение");
                        DungeonGenerator();

                        
                    }
                }
                else
                {
                   heroPower = heroPower + dungeon[roomNumber - 1].BossPower;
                   lbPower.Content = heroPower.ToString(); 
                   roomCleared = ++roomCleared;
                }

                dungeon[roomNumber - 1].IsChecked = true;
                
            }
            else
            {
                lbIndicator.Content = "КОМНАТА ПУСТА!";
            }
            lbRoomClearCount.Content = roomCleared;

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
                tbTest.Text = tbTest.Text + dungeon[i].BossPower + " " + dungeon[i].RoomContents + " " + dungeon[i].IsChecked.ToString() + "\n";
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
    }
    
}
