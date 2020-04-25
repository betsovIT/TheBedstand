namespace TheBedstand.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TheBedstand.Data.Models;
    using TheBedstand.Data.Models.Enums;

    public class AuthorSeeder : ISeeder
    {
        private readonly Author[] initialAuthors = new Author[]
        {
            // Id will be set to 1
            new Author
            {
                PersonalName = "J. R. R.",
                Surname = "Tolkien",
                Biography = @"John Ronald Reuel Tolkien, CBE was an English writer, poet, WWI veteran (a First Lieutenant in the Lancashire Fusiliers, British Army), philologist, and university professor, best known as the author of the high fantasy classic works The Hobbit and The Lord of the Rings .Tolkien was Rawlinson and Bosworth Professor of Anglo-Saxon at Oxford from 1925 to 1945, and Merton Professor of English language and literature from 1945 to 1959. He was a close friend of C.S. Lewis.",
                DateOfBirth = new DateTime(1892, 1, 3),
                Country = Country.GB,
                ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584093634/author_photos/656983_bpzsbz.jpg",
                ImageId = "author_photos/656983_bpzsbz",
            },

            // Id will be set to 2
            new Author
            {
                PersonalName = "James",
                Surname = "Clavell",
                Biography = "James Clavell, born Charles Edmund Dumaresq Clavell was a British novelist, screenwriter, director and World War II veteran and POW. Clavell is best known for his epic Asian Saga series of novels and their televised adaptations, along with such films as The Great Escape, The Fly and To Sir, with Love.",
                DateOfBirth = new DateTime(1924, 10, 10),
                Country = Country.GB,
                ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584093854/author_photos/656983_bpzsbz.jpg",
                ImageId = "author_photos/656983_bpzsbz",
            },

            // Id will be set to 3
            new Author
            {
                PersonalName = "Isaac",
                Surname = "Asimov",
                Biography = "Isaac Asimov was a Russian-born, American author, a professor of biochemistry, and a highly successful writer, best known for his works of science fiction and for his popular science books.Professor Asimov is generally considered one of the most prolific writers of all time, having written or edited more than 500 books and an estimated 90,000 letters and postcards. He has works published in nine of the ten major categories of the Dewey Decimal System (lacking only an entry in the 100s category of Philosophy).",
                DateOfBirth = new DateTime(1920, 1, 2),
                Country = Country.US,
                ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584093918/author_photos/16667_cqsabc.jpg",
                ImageId = "author_photos/16667_cqsabc",
            },

            // Id will be set to 4
            new Author
            {
                PersonalName = "Stephen",
                Surname = "King",
                Biography = "Stephen Edwin King was born the second son of Donald and Nellie Ruth Pillsbury King. After his father left them when Stephen was two, he and his older brother, David, were raised by his mother. Parts of his childhood were spent in Fort Wayne, Indiana, where his father's family was at the time, and in Stratford, Connecticut. When Stephen was eleven, his mother brought her children back to Durham, Maine, for good. Her parents, Guy and Nellie Pillsbury, had become incapacitated with old age, and Ruth King was persuaded by her sisters to take over the physical care of them. Other family members provided a small house in Durham and financial support. After Stephen's grandparents passed away, Mrs. King found work in the kitchens of Pineland, a nearby residential facility for the mentally challenged." +
                "Stephen attended the grammar school in Durham and Lisbon Falls High School, graduating in 1966. From his sophomore year at the University of Maine at Orono, he wrote a weekly column for the school newspaper, THE MAINE CAMPUS. He was also active in student politics, serving as a member of the Student Senate. He came to support the anti-war movement on the Orono campus, arriving at his stance from a conservative view that the war in Vietnam was unconstitutional. He graduated in 1970, with a B.A. in English and qualified to teach on the high school level. A draft board examination immediately post-graduation found him 4-F on grounds of high blood pressure, limited vision, flat feet, and punctured eardrums." +
                "He met Tabitha Spruce in the stacks of the Fogler Library at the University, where they both worked as students; they married in January of 1971. As Stephen was unable to find placement as a teacher immediately, the Kings lived on his earnings as a laborer at an industrial laundry, and her student loan and savings, with an occasional boost from a short story sale to men's magazines." +
                @"Stephen made his first professional short story sale ""The Glass Floor"" to Startling Mystery Stories in 1967. Throughout the early years of his marriage, he continued to sell stories to men's magazines. Many were gathered into the Night Shift collection or appeared in other anthologies." +
                "In the fall of 1971, Stephen began teaching English at Hampden Academy, the public high school in Hampden, Maine.Writing in the evenings and on the weekends, he continued to produce short stories and to work on novels.",
                DateOfBirth = new DateTime(1947, 9, 21),
                Country = Country.US,
                ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584094341/author_photos/3389_m5mbsw.jpg",
                ImageId = "author_photos/3389_m5mbsw",
            },
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Authors.Any())
            {
                return;
            }

            foreach (var author in this.initialAuthors)
            {
                await dbContext.Authors.AddAsync(author);
            }
        }
    }
}
