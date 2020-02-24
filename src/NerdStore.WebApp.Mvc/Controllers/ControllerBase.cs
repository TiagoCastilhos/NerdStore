using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.WebApp.Mvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly DomainNotificationHandler _domainNotificationHandler;

        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        protected ControllerBase(IMediatorHandler mediatorHandler, INotificationHandler<DomainNotification> domainNotificationHandler)
        {
            _mediatorHandler = mediatorHandler;
            _domainNotificationHandler = (DomainNotificationHandler)domainNotificationHandler;
        }

        protected bool OperacaoValida()
            => !_domainNotificationHandler.TemNotificacao();

        protected IEnumerable<string> ObterMensagensDeErro()
            => _domainNotificationHandler.ObterNotificacoes().Select(c => c.Value).ToList();


        protected void NotificarErro(string codigo, string mensagem)
            => _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));

    }
}