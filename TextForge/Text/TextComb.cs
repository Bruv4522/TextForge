using System.Text;

namespace TextForge.Text;

public class TextComb
{
    public Task<string> Combine(string a, string b) 
    {
        return Task.Run(() => {
            Random random = new();
            StringBuilder result = new(a.Length);

            List<string> aList = a.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .ToList();

            List<string> bList = b.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .ToList();

            for (int i = 0; i < aList.Count + bList.Count * 100; i++) 
            {
                int choice = random.Next(0, 2);

                if (choice == 0)
                {
                    result.Append(bList[random.Next(bList.Count)]);
                } else
                {
                    result.Append(aList[random.Next(aList.Count)]);
                }

                result.Append(" ");
            }

            return result.ToString(); 
        });
    }
}
