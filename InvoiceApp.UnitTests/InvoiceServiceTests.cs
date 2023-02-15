using InvoiceApp.Domain.Aggregates.InvoiceAggregate;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate.Entities;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;
using InvoiceApp.Domain.Interfaces;
using InvoiceApp.Domain.Services;
using Moq;

namespace InvoiceApp.UnitTests
{
    public class InvoiceServiceTests
    {
        private readonly InvoiceService _sut;

        private readonly Mock<ICosmosConnection> _mockcosmos = new Mock<ICosmosConnection>();

        const string testDescription = "testdescription";
        public InvoiceServiceTests()
        {
            _sut = new InvoiceService(_mockcosmos.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddInvoice_WhenInvoiceIsValid()
        {
            // Arrange
            var newInvoiceItems = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 2, 2) };
            var newInvoice = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItems.Sum(x => x.Amount), newInvoiceItems);

            _mockcosmos.Setup(x => x.AddAsync(newInvoice)).ReturnsAsync(newInvoice);

            // Act
            var invoice = await _sut.CreateAsync(newInvoice);

            // Assert
            Assert.Equal(invoice, newInvoice);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllInvoices()
        {
            // Arrange
            var newInvoiceItem1 = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 1, 1) };
            var newInvoiceItem2 = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 2, 2) };
            var newInvoiceItem3 = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 3, 3) };

            var newInvoice1 = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItem1.Sum(x => x.Amount), newInvoiceItem1);
            var newInvoice2 = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItem2.Sum(x => x.Amount), newInvoiceItem2);
            var newInvoice3 = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItem3.Sum(x => x.Amount), newInvoiceItem3);

            _mockcosmos.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Invoice> { newInvoice1, newInvoice2, newInvoice3 });

            // Act
            var invoices = await _sut.GetAllAsync();

            // Assert
            Assert.Equal(3, invoices.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectInvoice_WhenIdIsValid()
        {
            // Arrange
            var newInvoiceItems = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 2, 2) };
            var newInvoice = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItems.Sum(x => x.Amount), newInvoiceItems);

            _mockcosmos.Setup(x => x.GetAsync(newInvoice.Id.Value)).ReturnsAsync(newInvoice);


            // Act
            var invoice = await _sut.GetAsync(newInvoice.Id.Value);

            // Assert
            Assert.Equal(invoice, newInvoice);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateInvoice_WhenInvoiceIsValid()
        {
            // Arrange
            var newInvoiceItems = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 2, 2) };
            var newInvoice = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItems.Sum(x => x.Amount), newInvoiceItems);

            _mockcosmos.Setup(x => x.GetAsync(newInvoice.Id.Value)).ReturnsAsync(newInvoice);
            _mockcosmos.Setup(r => r.UpdateAsync(It.IsAny<Invoice>()));


            // Act
            await _sut.UpdateAsync(newInvoice);

            // Assert
            _mockcosmos.Verify(r => r.UpdateAsync(newInvoice));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteInvoice_WhenIdIsValid()
        {
            // Arrange
            var newInvoiceItems = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 2, 2) };
            var newInvoice = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItems.Sum(x => x.Amount), newInvoiceItems);

            _mockcosmos.Setup(r => r.DeleteAsync(It.IsAny<string>()));

            // Act
            await _sut.DeleteAsync(newInvoice.Id.Value);

            // Assert
            _mockcosmos.Verify(r => r.DeleteAsync(newInvoice.Id.Value));
        }

        [Fact]
        public async Task GetMultipleAsync_ShouldReturnAllInvoices_WithDefaultQuery()
        {
            // Arrange
            var newInvoiceItem1 = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 1, 1) };
            var newInvoiceItem2 = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 2, 2) };
            var newInvoiceItem3 = new List<InvoiceItem> { new InvoiceItem(InvoiceItemId.CreateUnique(), 3, 3) };

            var newInvoice1 = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItem1.Sum(x => x.Amount), newInvoiceItem1);
            var newInvoice2 = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItem2.Sum(x => x.Amount), newInvoiceItem2);
            var newInvoice3 = new Invoice(InvoiceId.CreateUnique(), testDescription, newInvoiceItem3.Sum(x => x.Amount), newInvoiceItem3);

            _mockcosmos.Setup(x => x.GetMultipleAsync("select * from c")).ReturnsAsync(new List<Invoice> { newInvoice1, newInvoice2, newInvoice3 });

            // Act
            var invoices = await _sut.GetMultipleAsync("select * from c");

            // Assert
            Assert.Equal(3, invoices.ToList().Count);
        }
    }
}