namespace Vehicles.Core.Domain
{
    public class Model : EntityBase
    {
        public string Name { get; private set; } = null!;
        public long MarkId { get; private set; }

        public virtual Mark Mark { get; private set; } = null!;

        public virtual ICollection<Vehicle> Vehicles { get; private set; }

        public Model()
        {
            Vehicles = new List<Vehicle>();
        }

        public Model(string name, long markId)
        {
            name.ThrowIfNull(() => throw new Exception(DomainResources.ModelNameNeedsToBeSpecifiedException))
                .IfWhiteSpace();

            markId.Throw(() => throw new Exception(DomainResources.MarkIdNeedsToBeSpecifiedException))
              .IfNegativeOrZero();

            Name = name;
            MarkId = markId;
            Vehicles = new List<Vehicle>();
        }

        public Model(long id, string name, long markId)
        {
            id.Throw(() => throw new Exception(DomainResources.ModelIdNeedsToBeSpecifiedException))
              .IfNegativeOrZero();

            name.ThrowIfNull(() => throw new Exception(DomainResources.ModelNameNeedsToBeSpecifiedException))
                .IfWhiteSpace();

            markId.Throw(() => throw new Exception(DomainResources.MarkIdNeedsToBeSpecifiedException))
             .IfNegativeOrZero();

            Id = id;
            Name = name;
            MarkId = markId;
            Vehicles = new List<Vehicle>();
        }

        public void UpdateModel(string name, long markId)
        {
            name.ThrowIfNull(() => throw new Exception(DomainResources.ModelNameNeedsToBeSpecifiedException))
                .IfWhiteSpace();

            markId.Throw(() => throw new Exception(DomainResources.MarkIdNeedsToBeSpecifiedException))
              .IfNegativeOrZero();

            Name = name;
            MarkId = markId;
        }
    }
}
