using BusinessObject;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMember(int memberID);
        void DeleteMember(int memberID);
        void InsertMember(MemberObject member);
        void UpdateMember(MemberObject member);

    }
}
