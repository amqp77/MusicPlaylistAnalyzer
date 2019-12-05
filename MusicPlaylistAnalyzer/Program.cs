using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPlaylistAnalyzer
{
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Size { get; set; }
        public int Time { get; set; }
        public int Year { get; set; }
        public int Plays { get; set; }
        public string getData()
        {
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }
        public string song(string songName, string artistName, string albumName, string genreName, int fileSize, int songLength, int songYear, int numOfPlays)
        {
            Name = songName;
            Artist = artistName;
            Album = albumName;
            Genre = genreName;
            Size = fileSize;
            Time = songLength;
            Year = songYear;
            Plays = numOfPlays;
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Use: MusicPlaylistAnalyzer.exe <music_playlist_file_path> <report_file_path>");
                Environment.Exit(0);
            }
            List<Song> writerstat = new List<Song>();
            try
            {
                using (StreamReader reader = new StreamReader(args[0]))
                {
                    var linecount = 0;
                    var ItemRow = 8;
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        linecount++;
                        string[] values = line.Split('\t');
                        if (values.Length < 8)
                        {
                            Console.WriteLine($"\nError: Row {linecount} contains {values.Length} values. It is supposed to contain {ItemRow}. ");
                            Environment.Exit(0);

                        }
                        else if (values.Length > 8)
                        {
                            Console.WriteLine($"\nError: Row {linecount} contains {values.Length} values.");
                            Environment.Exit(0);
                        }
                        try
                        {
                            var musicData = new Song
                            {
                                Name = values[0],
                                Artist = values[1],
                                Album = values[2],
                                Genre = values[3],
                                Size = Int32.Parse(values[4]),
                                Time = Int32.Parse(values[5]),
                                Year = Int32.Parse(values[6]),
                                Plays = Int32.Parse(values[7])
                            };
                            writerstat.Add(musicData);
                        }

                        catch (Exception)
                        {
                            Console.WriteLine($"Error: Row {linecount} has the wrong data type.");
                            Environment.Exit(0);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception reader)
            {
                Console.WriteLine("Error: " + reader.Message);
                Environment.Exit(0);
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(args[1]))
                {
                    Song[] songs = writerstat.ToArray();
                    int count = 0;
                    writer.WriteLine("Music Playlist Report:\n");
                    var greater200 = from song in songs where song.Plays >= 200 select song;
                    writer.WriteLine("\n");