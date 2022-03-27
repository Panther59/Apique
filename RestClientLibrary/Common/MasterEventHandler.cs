using RestClientLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.Common
{
    public class MasterEventHandler
    {
        public delegate void OnSaveSessionHandler(TransactionViewModel session);
        public static event OnSaveSessionHandler SaveSession;

        public static void RaiseSaveSessionEvent(TransactionViewModel session)
        {
            if (SaveSession != null)
            {
                SaveSession(session);
            }
        }

        //public delegate void OnSessionSelectedHandler(RestClientLibrary.ViewModel.SessionHistoryViewModel session, string type);
        //public static event OnSessionSelectedHandler SessionSelected;

        //public static void RaiseSessionSelectedEvent(RestClientLibrary.ViewModel.SessionHistoryViewModel session, string type)
        //{
        //    if (SessionSelected!= null)
        //    {
        //        SessionSelected(session, type);
        //    }
        //}

        public delegate void OnRemoveSessionHandler(RestClientLibrary.ViewModel.SessionHistoryViewModel session);
        public static event OnRemoveSessionHandler RemoveSession;

        public static void RaiseRemoveSessionEvent(RestClientLibrary.ViewModel.SessionHistoryViewModel session)
        {
            if (RemoveSession != null)
            {
                RemoveSession(session);
            }
        }

        public delegate void OnSaveRequestInitiatedHandler(CategoryViewModel ctg, TransactionViewModel req);
        public static event OnSaveRequestInitiatedHandler SaveRequestInitiated;

        //public static void RaiseSaveRequestInitiatedEvent(Category ctg, Transaction req)
        //{
        //    if (SaveRequestInitiated!= null)
        //    {
        //        SaveRequestInitiated(ctg, req);
        //    }
        //}

        //public delegate void OnRequestSelectedHandler(Transaction req);
        //public static event OnRequestSelectedHandler RequestSelected;

        //public static void RaiseRequestSelectedEvent(Transaction req)
        //{
        //    if (RequestSelected!= null)
        //    {
        //        RequestSelected(req);
        //    }
        //}

        //public delegate void OnRemoveRequestHandler(TransactionViewModel req);
        //public static event OnRemoveRequestHandler RemoveRequest;

        //public static void RaiseRemoveRequestEvent(TransactionViewModel req)
        //{
        //    if (RemoveRequest != null)
        //    {
        //        RemoveRequest(req);
        //    }
        //}

        //public delegate void OnRemoveCategoryHandler(CategoryViewModel ctg);
        //public static event OnRemoveCategoryHandler RemoveCategory;

        //public static void RaiseRemoveCategoryEvent(CategoryViewModel ctg)
        //{
        //    if (RemoveCategory != null)
        //    {
        //        RemoveCategory(ctg);
        //    }
        //}

        public delegate void OnResponseReceivedHandler();
        public static event OnResponseReceivedHandler ResponseReceived;

        public static void RaiseResponseReceivedEvent()
        {
            if (ResponseReceived != null)
            {
                ResponseReceived();
            }
        }
        
        public delegate void OnReloadSavedRequestHandler();
        public static event OnReloadSavedRequestHandler ReloadSavedRequest;
        public static void RaiseReloadSavedRequestEvent()
        {
            if (ReloadSavedRequest != null)
            {
                ReloadSavedRequest();
            }
        }
    }
}
