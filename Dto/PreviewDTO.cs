namespace CapitalApi.Dto
{
    public class PreviewDTO

    {
        public Guid Id { get; set; }
        public ProgramDTO? Program { get; set; }

        public List<TemplateDTO>? Templates { get; set; }
        public WorkflowDTO? Workflow { get; set; }
    }



}
