using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plan.API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Testy.Controllers
{
    [TestClass]
    public class ErrorControllerTesty
    {
        private ErrorController _controller;

        public ErrorControllerTesty()
        {
            _controller = new ErrorController();
        }

        [TestMethod]
        public void MyTestMethod()
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }
    }
}
