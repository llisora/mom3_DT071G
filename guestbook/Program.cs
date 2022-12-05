/*
    Simulate a guestbook where you can write and delete posts, with author.
    The datafile,'guestbook.json', created is in the format of Json.

    Written by Lisa Bäcklin / Mid Sweden University
*/

using System;
using System.Collections.Generic;
using System.IO;

using System.Text.Json;

namespace posts
{
    public class GuestBook
    {

        private string filename = @"guestbook.json";
        private List<Post> posts = new List<Post>();

        public GuestBook()
        {
            if (File.Exists(@"guestbook.json") == true)
            { // If stored json data exists then read
                string jsonString = File.ReadAllText(filename);
                posts = JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }

        /*Add post*/
        public Post addPost(Post post)
        {
            posts.Add(post);
            marshal();
            return post;
        }

        /*Delete post*/
        public int delPost(int index)
        {
            posts.RemoveAt(index);
            marshal();
            return index;
        }

        /*Get all posts*/
        public List<Post> getPosts()
        {
            return posts;
        }

        private void marshal()
        {
            // Serialize all the objects and save to file
            var jsonString = JsonSerializer.Serialize(posts);
            File.WriteAllText(filename, jsonString);
        }
    }

    public class Post
    {
        /*Set and get for post and author*/
        private string post_no;
        public string Post_no
        {
            set { this.post_no = value; }
            get { return this.post_no; }
        }

        private string author;
        public string Author
        {
            set { this.author = value; }
            get { return this.author; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            GuestBook guestbook = new GuestBook();
            int i = 0;

            while (true)
            {
                Console.Clear(); Console.CursorVisible = false;
                Console.WriteLine("G Ä S T B O K E N \n\n");

                Console.WriteLine("1. Lägg till inlägg");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("X. Avsluta\n");

                i = 0;
                foreach (Post post in guestbook.getPosts())
                {
                    /*Write out in console*/
                    Console.WriteLine("[" + i++ + "] " + post.Author + " - " + post.Post_no);
                }

                int inp = (int)Console.ReadKey(true).Key;
                switch (inp)
                {
                    case '1':
                        Console.Clear();
                        /*Case 1 - write a post*/
                        Console.CursorVisible = true;
                        Console.Write("Ange författare: ");
                        string author = Console.ReadLine();
                        if (String.IsNullOrEmpty(author))
                        {
                            /*If string is empty - this message appears*/
                            Console.Write("Du måste fylla i ett namn! ");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Skriv inlägg: ");
                        string postno = Console.ReadLine();
                        if (String.IsNullOrEmpty(postno))
                        {
                            /*If string is empty - this message appears*/
                            Console.Write("Du måste skriva något! ");
                            Console.ReadKey();
                            break;
                        }
                        /*Create new json-object*/
                        Post obj = new Post();
                        obj.Post_no = postno;
                        obj.Author = author;

                        guestbook.addPost(obj);

                        break;
                    case '2':
                        Console.Clear();
                        /*Case 2 - delete a post*/
                        Console.CursorVisible = true;
                        Console.Write("Ange index att radera: ");
                        string index = Console.ReadLine();
                        guestbook.delPost(Convert.ToInt32(index));
                        break;
                    case 88:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
