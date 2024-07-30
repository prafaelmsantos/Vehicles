namespace Vehicles.Core
{
    /// <summary>
    /// Class Domain Resource for domain exception
    /// </summary>

    public static class DomainResources
    {
        #region Mark
        public static readonly string MarkNotFoundException = "Marca não encontrada.";

        public static readonly string MarkIdNeedsToBeSpecifiedException = "O id da marca é invalido.";
        public static readonly string MarkNameNeedsToBeSpecifiedException = "O nome da marca é invalido.";
        public static readonly string MarkAlreadyExistsException = "A marca já existe.";

        public static readonly string AddMarkAsyncException = "Erro ao tentar criar a marca.";
        public static readonly string UpdateMarkAsyncException = "Erro ao tentar atualizar a marca.";
        public static readonly string GetAllMarksAsyncException = "Erro ao tentar encontrar marcas.";
        public static readonly string GetMarkByIdAsyncException = "Erro ao tentar encontrar marca por id.";
        public static readonly string DeleteMarksAsyncException = "Erro ao tentar apagar marcas.";
        #endregion

        #region Model
        public static readonly string ModelNotFoundException = "Modelo não encontrado.";

        public static readonly string ModelIdNeedsToBeSpecifiedException = "O id do modelo é invalido.";
        public static readonly string ModelNameNeedsToBeSpecifiedException = "O nome do modelo é invalido.";
        public static readonly string ModelAlreadyExistsException = "O modelo já existe.";

        public static readonly string AddModelAsyncException = "Erro ao tentar criar o modelo.";
        public static readonly string UpdateModelAsyncException = "Erro ao tentar atualizar o modelo.";
        public static readonly string GetAllModelsAsyncException = "Erro ao tentar encontrar modelos.";
        public static readonly string GetModelByIdAsyncException = "Erro ao tentar encontrar modelo por id.";
        public static readonly string GetModelsByMarkIdAsyncException = "Erro ao tentar encontrar modelos por marca id.";
        public static readonly string DeleteModelsAsyncException = "Erro ao tentar apagar modelos.";
        #endregion

        #region Vehicle

        public static readonly string VehicleNotFoundException = "Veículo não encontrado.";

        public static readonly string VehicleIdNeedsToBeSpecifiedException = "O id da veículo é invalido.";
        public static readonly string VehicleModelIdNeedsToBeSpecifiedException = "O id do modelo da veículo é invalido.";
        public static readonly string VehicleFuelTypeNeedsToBeSpecifiedException = "O tipo de combustível do veículo é invalido.";
        public static readonly string VehiclePriceNeedsToBeSpecifiedException = "O preço do veículo é invalido.";
        public static readonly string VehicleMileageNeedsToBeSpecifiedException = "O numero de quilómetros do veículo é invalido.";
        public static readonly string VehicleYearNeedsToBeSpecifiedException = "O ano do veículo é invalido.";
        public static readonly string VehicleDoorsNeedsToBeSpecifiedException = "O numero de portas do veículo é invalido.";
        public static readonly string VehicleTransmissionNeedsToBeSpecifiedException = "A transmissão do veículo é invalida.";
        public static readonly string VehicleEngineSizeNeedsToBeSpecifiedException = "O tamanho do motor do veículo é invalido.";
        public static readonly string VehiclePowerNeedsToBeSpecifiedException = "A potência do veículo é invalida.";

        public static readonly string GetAllVehiclesAsyncException = "Erro ao tentar encontrar veículos.";
        public static readonly string GetVehicleByIdAsyncException = "Erro ao tentar encontrar veículo por id.";
        public static readonly string GetVehicleCountersAsyncException = "Erro ao tentar encontrar o numero total de veículos.";
        public static readonly string GetAllVehiclesWithYearComparisonAsyncException = "Erro ao tentar encontrar dados de vendas (por ano) de veículos.";
        public static readonly string GetAllVehiclesWithMonthComparisonAsyncException = "Erro ao tentar encontrar dados de vendas (por mês) de veículos.";
        public static readonly string GetVehiclePieStatisticsAsyncException = "Erro ao tentar encontrar dados de vendas de veículos.";
        public static readonly string AddVehicleAsyncException = "Erro ao tentar criar o veículo.";
        public static readonly string UpdateVehicleAsyncException = "Erro ao tentar atualizar o veículo.";
        public static readonly string DeleteVehiclesAsyncException = "Erro ao tentar apagar veículos.";

        #endregion

        #region Vehicle Image

        public static readonly string VehicleImageUrlNeedsToBeSpecifiedException = "O endereço da imagem do veículo é invalido.";

        #endregion
    }
}
