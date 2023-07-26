////using AutoMapper;
////using Hvex.Domain.Dto;
////using Hvex.Domain.Entity;
////using Hvex.Domain.Interface.Services;
////using Hvex.Domain.Services;
//using Moq;

//namespace TestProject1
//{
//    public class Tests
//    {

//        ITestService service;

//        [SetUp]
//        public void Setup()
//        {
//            Mock<IMapper> mapper = new Mock<IMapper>();
//            Mock<ITestService> testService = new Mock<ITestService>();
//            service = new TestService(testService.Object);

//        }

//        [Test]
//        public async Task Test1()
//        {
//            int id = 2;

//            TestDto result = await service.BuscarTestPorIdAsync(id);
//            Assert.Equals(2, result);
//        }
//    }
////}