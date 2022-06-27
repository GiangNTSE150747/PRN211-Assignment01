using BusinessObject;
using DataAccess.DataAccess;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int memberID) => MemberDBContext.Instance.Remove(memberID);
        public MemberObject GetMember(int memberID) => MemberDBContext.Instance.GetMemberByID(memberID);

        public IEnumerable<MemberObject> GetMembers() => MemberDBContext.Instance.GetMemberList();

        public void InsertMember(MemberObject member) => MemberDBContext.Instance.AddNew(member);

        public void UpdateMember(MemberObject member) => MemberDBContext.Instance.Update(member);
    }
}
