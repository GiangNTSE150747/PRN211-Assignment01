using DataAccess.DataAccess;
using DataAccess.Repository;

public class Program
{
    public static void Main()
    {
       MemberRepository memberRepository = new MemberRepository();
        

        System.Console.WriteLine(memberRepository.GetMember(1));
        System.Console.ReadLine();

    }
}