using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstTest;

public class Student
{
    public string? First { get; set; }
    public string? Last { get; set; }
    public int ID { get; set; }
    public List<int>? Scoress { get; set; }
    public char LastInitial => string.IsNullOrWhiteSpace(Last) ? '?' : Last[0];
}

public class SeedData
{
    List<Student> Students = new List<Student>
    {
        new Student {First="Svetlana", Last="Omelchenko", ID=111, Scoress= new List<int> {97, 92, 81, 60}},
        new Student {First="Claire", Last="O'Donnell", ID=112, Scoress= new List<int> {75, 84, 91, 39}},
        new Student {First="Sven", Last="Mortensen", ID=113, Scoress= new List<int> {88, 94, 65, 91}},
        new Student {First="Cesar", Last="Garcia", ID=114, Scoress= new List<int> {97, 89, 85, 82}},
        new Student {First="Debra", Last="Garcia", ID=115, Scoress= new List<int> {35, 72, 91, 70}},
        // generate new students with different first and last names and id
        new Student {First="Svetlana", Last="Omelchenko", ID=116, Scoress= new List<int> {97, 92, 81, 60}},

        new Student {First="Claire", Last="O'Donnell", ID=117, Scoress= new List<int> {75, 84, 91, 39}},
        new Student {First="Sven", Last="Mortensen", ID=118, Scoress= new List<int> {88, 94, 65, 91}},
        new Student {First="Cesar", Last="Garcia", ID=119, Scoress= new List<int> {97, 89, 85, 82}},
                new Student {First="Debra", Last="Garcia", ID=120, Scoress= new List<int> {35, 72, 91, 70}}


    };

}

