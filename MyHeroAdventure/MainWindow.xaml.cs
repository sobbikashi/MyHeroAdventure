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
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }

        public int heroPower = 25;
        public int roomCleared = 10;
        public int artifactsCount = 3;        
        public Room[] dungeon = new Room[10];
        
        Random rnd = new Random(); 
        public List<int> artifactIndex = new List<int>();

        public string BossNameGenerator(int power)
        {
            string bossName = "Скелет";
            if ((power > 0) && (power <= 10))
            {
                bossName = "Здоровенная Бешеная Цыпа";
            } else if ((power > 10) && (power <= 20))
            {
                bossName = "3872 орка";
            } else if ((power > 20) && (power <= 30))
            {
                bossName = "Блуждающий Нос";
            } else if ((power > 30) && (power <= 40))
            {
                bossName = "Страховой Агент";
            } else if ((power > 40) && (power <= 50))
            {
                bossName = "Пикачу с Топором";
            } else if ((power > 50) && (power <= 60))
            {
                bossName = "Милый Кролик";
            } else if ((power > 60) && (power <= 70))
            {
                bossName = "Шовинистский Боров";
            } else if ((power > 70) && (power <= 80))
            {
                bossName = "Лососилиск";
            } else if ((power > 80) && (power <= 90))
            {
                bossName = "Челведведосвин";
            } else if ((power > 90) && (power <= 100))
            {
                bossName = "Газебо";
            }                
            return bossName;
        }
        public string ArtifactNameGenerator(int power)
        {
            string artifactName = "Скелет";
            if ((power > 0) && (power <= 10)) artifactName = "Банка Яги";
            if ((power > 10) && (power <= 20)) artifactName = "Трусы с начёсом";
            if ((power > 20) && (power <= 30)) artifactName = "Кольцо ВсеЗдрасти";
            if ((power > 30) && (power <= 40)) artifactName = "Vrblther's Vibroblade";
            if ((power > 40) && (power <= 50)) artifactName = "Демократизатор";
            if ((power > 50) && (power <= 60)) artifactName = "Розовые Очки";
            if ((power > 60) && (power <= 70)) artifactName = "Шашка Чапаева";
            if ((power > 70) && (power <= 80)) artifactName = "Синяя Изолента";
            if ((power > 80) && (power <= 90)) artifactName = "Бензопила Кровавого Расчленения";
            if ((power > 90) && (power <= 100)) artifactName = "Б.Ф.Г.";
            return artifactName;
        }
        public void DungeonGenerator()
        {
            heroPower = 25;
            roomCleared = 10;
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
            }

            lbPower.Content = heroPower.ToString();
            lbIndicator.Content = "ВЫБЕРИ СВОЙ ПУТЬ!";

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
                   tbStatus.Text = "Вы нашли артефакт " + dungeon[roomNumber-1].Name.ToString() + " силой " + dungeon[roomNumber - 1].BossPower.ToString();
                   heroPower = heroPower + dungeon[roomNumber - 1].BossPower;
                   lbPower.Content = heroPower.ToString(); 
                   roomCleared = --roomCleared;                   
                }

                dungeon[roomNumber - 1].IsChecked = true;
                
            }
            else
            {
                lbIndicator.Content = "КОМНАТА ПУСТА!";
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
    }
    
}
