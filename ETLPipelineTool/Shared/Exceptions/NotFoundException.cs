namespace ETLPipelineTool.Shared.Exceptions
{
    public class NotFoundException<TIdentity> : BusinessException
    {
        public NotFoundException(Type type, TIdentity id)
            : base(string.Format(Constants.MessageTemplate.NOT_FOUND, type.Name, id)) { }

        public NotFoundException(Type type, TIdentity id, Exception? innerException)
            : base(
                string.Format(Constants.MessageTemplate.NOT_FOUND, type.Name, id),
                innerException
            ) { }
    }

    public class NotFoundException(Type type, string id)
        : BusinessException(
            string.Format(
                Constants.MessageTemplate.NOT_FOUND,
                type.Name,
                SanitizerHelper.SanitizeAllHtml(id)
            )
        ) { }
}
