using System;
using System.Collections.Generic;
using System.IO;

namespace test
{
    //Ужасный код//
    class DB
    {
        public List<string> A { get; set; }
        public List<string> B { get; set; }
        public List<string> C { get; set; }
        public List<string> D { get; set; }
        public List<string> E { get; set; }
        public List<string> F { get; set; }
        public List<string> G { get; set; }
        public DB(string path)
        {
            A = new List<string>();
            B = new List<string>();
            C = new List<string>();
            D = new List<string>();
            E = new List<string>();
            F = new List<string>();
            G = new List<string>();
            ReadDB(path);
        }
        public void ReadDB(string path)
        {
            if(! File.Exists(path))
            {
                File.Create(path).Close();
            }

            StreamReader f = new StreamReader(path);
            string line;
            while((line = f.ReadLine()) != null)
            {
                string[] temp = line.Split("\t");
                A.Add(temp[0]);
                B.Add(temp[1]);
                C.Add(temp[2]);
                D.Add(temp[3]);
                E.Add(temp[4]);
                F.Add(temp[5]);
                G.Add(temp[6]);
            }
            f.Close();
        }
        public void AddLineToDB(int pos, string[] values)
        {
            if (values.Length < 7)
                throw new Exception("Index was out of range");

            A.Insert(pos, values[0]);
            B.Insert(pos, values[1]);
            C.Insert(pos, values[2]);
            D.Insert(pos, values[3]);
            E.Insert(pos, values[4]);
            F.Insert(pos, values[5]);
            G.Insert(pos, values[6]);
        }
        public void AddLineToDB(string[] values)
        {
            if(values.Length < 7)
                throw new Exception("Index was out of range");

            A.Add(values[0]);
            B.Add(values[1]);
            C.Add(values[2]);
            D.Add(values[3]);
            E.Add(values[4]);
            F.Add(values[5]);
            G.Add(values[6]);
        }
        public void ShowDB(int pos)
        {
            Console.WriteLine("id " + (pos + 1) + ":\t" + A[pos] + "\t" + B[pos] + "\t" + C[pos] + "\t" + D[pos] + "\t" + E[pos] + "\t" + F[pos] +"\t" + G[pos]);
        }   
        public void ShowDB()
        {
            Console.WriteLine("     \tA\tB\tC\tD\tE\tF\tG");
            for (int i = 0; i < A.Count; i++)
                ShowDB(i);
        }
        public void UpdateLineDB(int pos, string[] values)
        {
            A[pos] = values[0];
            B[pos] = values[1];
            C[pos] = values[2];
            D[pos] = values[3];
            E[pos] = values[4];
            F[pos] = values[5];
            G[pos] = values[6];
        }
        public void DeleteLineDB(int pos)
        {
            A.RemoveAt(pos);
            B.RemoveAt(pos);
            C.RemoveAt(pos);
            D.RemoveAt(pos);
            E.RemoveAt(pos);
            F.RemoveAt(pos);
            G.RemoveAt(pos);
        }
        public void ExportToFile(string path)
        {
            StreamWriter f = new StreamWriter(path);
            for(int i = 0; i < A.Count; i++)
            {
                string line = A[i] + "\t" + B[i] + "\t" + C[i] + "\t" + D[i] + "\t" + E[i] + "\t" + F[i] + "\t" + G[i];
                f.WriteLine(line);
            }
            f.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = "test.tsv";
            DB db = new DB(path);

            while(true)
            {
                Console.WriteLine("Select option:\n1 - add\n2 - delete\n3 - update\n4 - show\n0 - exit\n");
                string sv1 = Console.ReadLine();
                if(sv1 == "1")
                {
                    Console.WriteLine("Select add option:\n1 - add to position\n2 - add to end\n");
                    string sv2 = Console.ReadLine();
                    if(sv2 == "1" || sv2 == "2")
                    {
                        string pos = "";
                        if (sv2 == "1")
                        {
                            Console.Write("Enter position: ");
                            pos = Console.ReadLine();
                        }

                        Console.Write("Enter data (format -> A B C D E F G):");
                        string values = Console.ReadLine();

                        try
                        {
                            if (sv2 == "1")
                                db.AddLineToDB(int.Parse(pos) - 1, values.Split(" "));
                            else
                                db.AddLineToDB(values.Split(" "));
                        } catch (Exception e)
                        {
                            Console.WriteLine("Your parameters is trash");
                            Console.WriteLine(e.Message + "\n");
                            continue;
                        }

                        Console.WriteLine("Success!\n");
                    } else
                    {
                        Console.WriteLine("There is no such option");
                        continue;
                    }
                } else if(sv1 == "2")
                {
                    Console.Write("Enter position: ");
                    string pos = Console.ReadLine();

                    try
                    {
                        db.DeleteLineDB(int.Parse(pos) - 1);
                    } catch(Exception e)
                    {
                        Console.WriteLine("Your parameters is trash");
                        Console.WriteLine(e.Message + "\n");
                        continue;
                    }

                    Console.WriteLine("Success!\n");
                } else if(sv1 == "3")
                {
                    Console.Write("Enter position: ");
                    string pos = Console.ReadLine();

                    Console.Write("Enter data (format -> A B C D E F G):");
                    string values = Console.ReadLine();

                    try
                    {
                        db.UpdateLineDB(int.Parse(pos) - 1, values.Split(" "));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Your parameters is trash");
                        Console.WriteLine(e.Message + "\n");
                        continue;
                    }

                    Console.WriteLine("Success!\n");
                }
                else if(sv1 == "4")
                {
                    Console.WriteLine("Select show option:\n1 - show db row\n2 - show full db\n");
                    string sv2 = Console.ReadLine();

                    if(sv2 != "1" && sv2 != "2")
                    {
                        Console.WriteLine("There is no such option");
                        continue;
                    }

                    string pos = "";
                    if(sv2 == "1")
                    {
                        Console.Write("Enter position: ");
                        pos = Console.ReadLine();
                    }

                    try
                    {
                        if (sv2 == "1")
                            db.ShowDB(int.Parse(pos) - 1);
                        else
                            db.ShowDB();
                    } catch(Exception e)
                    {
                        Console.WriteLine("There is no such row index");
                        Console.WriteLine(e.Message + "\n");
                        continue;
                    }

                    Console.WriteLine("Success!\n");
                }
                else if (sv1 == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("There is no such option");
                    continue;
                }

            }
            db.ExportToFile(path);

        }
    }
}
