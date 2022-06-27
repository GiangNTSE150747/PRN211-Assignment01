using DataAccess.Repository;

public class Program
{
    public static void Main()
    {
        MemberRepository memberRepository = new MemberRepository();
        memberRepository.DeleteMember(2);
    }
}