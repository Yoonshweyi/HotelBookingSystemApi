using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystem.Model;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Model.Setup.Payment;
using HotelBookingSystem.Model.Setup.Booking;
using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Shared;

namespace HotelBookingSystemApi.Feature.Payment
{
    public class DL_Payment
    {
        private readonly AppDbContext _appDbContext;
        public DL_Payment(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Get Payment
        public async Task<PaymentListResponseModel> GetPayment()
        {
            var responseModel = new PaymentListResponseModel();
            try
            {
                var payment = await _appDbContext
                    .TblPayments
                    .AsNoTracking()
                    .ToListAsync();
                responseModel.DataLst = payment
                    .Select(x => x.Change())
                    .ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<PaymentModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Get Payment With Pagination
        public async Task<PaymentListResponseModel> GetPayment(int PageSize, int PageNo)
        {
            var responseModel = new PaymentListResponseModel();
            try
            {
                var paymentList = _appDbContext.TblPayments.AsNoTracking();


                var payment = await paymentList
                    .Pagination(PageNo, PageSize)
                    .ToListAsync();

                var totalCount = await paymentList.CountAsync();
                var pageCount = totalCount / PageSize;
                if (totalCount % PageSize > 0)
                    pageCount++;

                responseModel.DataLst = payment.Select(x => x.Change()).ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
                responseModel.PageSetting = new PageSettingModel(PageNo, PageSize, pageCount, totalCount);
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<PaymentModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Get Payment PaymentId
        public async Task<PaymentResponseModel> GetPaymentById(int paymentId)
        {
            var responseModel = new PaymentResponseModel();
            try
            {
                var payment = await _appDbContext
                    .TblPayments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PaymentId == paymentId);
                if (payment is null)
                {
                    responseModel.MessageResponse = new MessageResponseModel
                        (false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                responseModel.Data = payment.Change();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.Data = new PaymentModel();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

        result:
            return responseModel;
        }
        #endregion

        #region Payment Create
        public async Task<MessageResponseModel> CreatePayment(PaymentModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                await _appDbContext.TblPayments.AddAsync(requestModel.Change());
                var result = await _appDbContext.SaveChangesAsync();
                responseModel = result > 0
                    ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                    : new MessageResponseModel(false, EnumStatus.Fail.ToString());
            }
            catch (Exception ex)
            {
                responseModel = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Payment Update
        public async Task<MessageResponseModel> UpdatePayment(int id, PaymentModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var payment = await _appDbContext.TblPayments.FirstOrDefaultAsync(x => x.PaymentId == id);
                if (payment is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    return responseModel;
                }

                #region Patch Method Validation Conditions

                if (!string.IsNullOrEmpty(requestModel.PaymentMethod))
                {
                    payment.PaymentMethod = requestModel.PaymentMethod;
                }
                if (requestModel.Amount != 0) // or another condition that signifies a valid price
                {
                    payment.Amount = requestModel.Amount;
                }
                if (requestModel.PaymentDate != null)
                {
                    payment.PaymentDate = requestModel.PaymentDate;
                }

                #endregion

                _appDbContext.Entry(payment).State = EntityState.Modified;
                var result = await _appDbContext.SaveChangesAsync();

                responseModel = result > 0
                    ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                    : new MessageResponseModel(false, EnumStatus.Fail.ToString());
            }
            catch (Exception ex)
            {
                responseModel = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Get Payment BookingId
        public async Task<PaymentResponseModel> GetPaymentByBookingId(int bookingId)
        {
            var responseModel = new PaymentResponseModel();
            try
            {
                var payment = await _appDbContext
                    .TblPayments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.BookingId == bookingId);
                if (payment is null)
                {
                    responseModel.MessageResponse = new MessageResponseModel
                        (false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                responseModel.Data = payment.Change();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.Data = new PaymentModel();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

        result:
            return responseModel;
        }
        #endregion

    }
}
