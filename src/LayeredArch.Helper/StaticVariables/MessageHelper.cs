namespace LayeredArch.Helper.StaticVariables
{
    public static class MessageHelper
    {
        #region Generic Validation Messages

        public const string RequiredField = "O campo {PropertyName} é obrigatório.";

        public const string RequiredFieldWithMinAndMax = "O campo {PropertyName} deve conter entre {MinLength} e {MaxLength} caracteres.";

        public const string RequireMax = "O campo {PropertyName} deve conter no máximo {MaxLength} caracteres.";

        public const string RequiredValidEmail = "Insira um endereço de e-mail válido.";

        public const string InvalidEnumValue = "O valor do campo {PropertyName} é inválido.";

        public static string RequiredStartDate(string className)
        {
            return $"A data de início do(a) {className} é obrigatória.";
        }

        public static string RequiredEndDate(string className)
        {
            return $"A data de fim do(a) {className} é obrigatória.";
        }

        public static string EndDateGreaterThanStartDate(string className)
        {
            return $"A data de fim do(a) {className} deve ser posterior à data de início.";
        }

        public static string StartDateGreaterThanNow(string className)
        {
            return $"A data de início do(a) {className} deve ser posterior à data atual.";
        }


        public static string RequiredCreatedDate(string className)
        {
            return $"A data de criação do(a) {className} é obrigatória.";
        }

        public static string RequiredCreatedBy(string className)
        {
            return $"O nome de utilizador de quem criou o(a) {className} é obrigatório.";
        }

        public static string RequiredRangeBetween(string atributo, int min, int max)
        {
            return $"O(a) {atributo} deve ter um valor entre {min} e {max}.";
        }


        #endregion

        #region Generic Controller Messages
        public static class GenericsControllers
        {
            public static string HasField(string field, string content)
            {
                return $"Este {field} {content} já existe!";
            }

            public static string SuccessCreationRegistration(string module, string content)
            {
                return $"O(a) {module} {content} foi criado(a) com sucesso!";
            }

            public static string SuccessChangeRegistration(string module, string content)
            {
                return $"O(a) {module} {content} foi alterado(a) com sucesso!";
            }

            public static string SuccessActivateRegistration(string module, string content)
            {
                return $"O(a) {module} {content} foi ativado(a) com sucesso!";
            }

            public static string SuccessRemovalRegistration(string module, string content)
            {
                return $"O(a) {module} {content} foi removido(a) com sucesso!";
            }

            public static string SuccessDisableRegistration(string module, string content)
            {
                return $"O(a) {module} {content} foi desabilitado(a) com sucesso!";
            }

            public static string ErrorInvalidValidationCriteria()
            {
                return $"Os critérios de validação não foram atendidos! Favor verificar!";
            }

            public static string ErrorLoadingData()
            {
                return $"Error a cerregar os dados!";
            }

            public static string ErrorParameterNotFound(string field, string content)
            {
                return $"O(a) {field} {content} não foi encontrado(a)!";
            }

        }
        #endregion

        #region Entity-Specific Controller Responses

        public static class PermissionControllerMessages
        {

            public const string PermissionEdited = "Permissão editada com sucesso!";

            public const string NoPermissionsSelected = "Nenhuma permissão foi selecionada!";
        }

        #endregion

        #region Entity-Specific Validations

        public static class Activity
        {
            public const string RequiredProject = "O nome do projeto associado é obrigatório.";
            public const string RequiredActivityType = "O tipo de atividade é obrigatório.";
            public const string RequiredUserId = "O utilizador que criou esta atividade é obrigatório.";
            public const string RequiredActivityStatus = "O status da atividade é obrigatório.";
        }

        public static class Review
        {
            public const string RequiredSprintId = "O nome do sprint é obrigatório.";
            public const string RequiredActivityId = "O nome da atividade é obrigatório.";
            public const string RequiredDeveloperId = "O nome do desenvolvedor é obrigatório.";
            public const string RequiredProductOwnerId = "O nome do product owner é obrigatório.";
            public const string RequiredReviewStatusId = "O status da review é obrigatório.";
        }

        public static class Retrospective
        {
            public const string RequiredSprintId = "O nome do sprint é obrigatório.";
            public const string RequiredRetrospectiveStatusId = "O status da retrospective é obrigatório.";
        }

        public static class Assessment
        {
            public const string RequiredEvaluatedId = "O desenvolvedor a ser avaliado é obrigatório.";
            public const string RequiredStatus = "O status da avaliação é obrigatório.";
            public const string FinalScoreRange = "A pontuação deve estar entre 0 e 100.";
        }

        public static class Action
        {
            public const string RequiredRetrospectiveId = "O nome da retrospective é obrigatório.";
            public const string RequiredOwnerId = "O nome do owner é obrigatório.";
            public const string RequiredStatusId = "O status da ação é obrigatório.";
            public const string RequiredProblemNote = "A descrição do problema é obrigatória.";
        }

        public static class KpiMeasure
        {
            public const string RequiredKpiId = "O kpi é obrigatório.";
            public const string ValueRange = "O valor deve estar entre 0 e 100.";
        }

        public static class Question
        {
            public const string RequiredKpiId = "O kpi é obrigatório.";
            public const string RequiredMetricId = "A métrica é obrigatória.";
            public const string RequiredAssessmentTypeId = "O tipo de avaliação é obrigatório.";
        }


        #endregion

        #region Generic Error Messages

        public static class GenericErrors
        {
            public static string CouldntFindEntity(string entityName, string fieldName, string Content)
            {
                return $"O(a) {entityName} com o(a) {fieldName} : {Content} não foi encontrado(a)!";
            }
            public static string CannotDeleteEntity(string entityName)
            {
                return $"Não é possível excluir o(a) {entityName} porque ele(a) está associado(a) a outros registros.";
            }

            public static string FieldAlreadyExists(string entityName, string fieldName, string Content)
            {
                return $"O(a) {entityName} com o(a) {fieldName} : {Content} já existe!";
            }

            public static string EntityAlreadyActive(string entityName)
            {
                return $"O(a) {entityName} já está ativo(a)!";
            }

            public static string EntityAlreadyInactive(string entityName)
            {
                return $"O(a) {entityName} já está inativo(a)!";
            }

        }
        #endregion

    }
}
