using BusinessObject;
using DataAccess.Repository;

namespace Ass01Solution
{
    public class Program
    {
        public static void Main()
        {
            MemberRepository memberRepository = new MemberRepository();
            //Đang bị lỗi connect k đc, coi lại Chỗ BaseADL, appsetting.json đồ
            memberRepository.GetMember(1);
        }
    }
}