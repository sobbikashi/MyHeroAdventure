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
            public bool IsArtifact { get; set; }
            public bool IsChecked { get; set; }
        }

        public int heroPower = 25;  
        public int artifactsCount = 1;
        public Room[] dungeon = new Room[10];
        
        Random rnd = new Random(); 
        public List<int> artifactIndex = new List<int>();
        public void DungeonGenerator()
        {
            //Генерируем количество артефактов
            int artifactsCount = rnd.Next(1, 9);
            //Генерируем индексы артефактов в массиве дверей
            for (int i = 0; i < artifactsCount+1; i++)
            {
                artifactIndex.Add(rnd.Next(1, 10));
            }
            tbPower.Text = heroPower.ToString();


            //Генерируем подземелье из боссов и артефактов
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < artifactIndex.Count; j++)
                {
                    if (i == artifactIndex[j])
                    {
                        dungeon[i].IsArtifact = true;
                    } 
                    dungeon[i].BossPower = rnd.Next(5, 100);
                    
                }
            }

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            DungeonGenerator();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            tbTest.Text = "";
            for (int i = 0; i < 10; i++)
            {
                tbTest.Text = tbTest.Text + dungeon[i].BossPower + " " + dungeon[i].IsArtifact.ToString() + " " + dungeon[i].IsChecked.ToString() + "\n";
            }
        }
    }
    
}
