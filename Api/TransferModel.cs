using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core;
using EBayFinancesApi.Core.Exceptions;
using EBayFinancesApi.Core.Models;
using EBayFinancesApi.Core.Request;
using EBayFinancesApi.Core.Response;
using EBayFinancesApi.Errors;
using EBayFinancesApi.Models;

namespace EBayFinancesApi.Api;

public sealed class TransferModel
{
    private readonly RawClient _rawClient;
    private readonly Server _server;
    private readonly AuthSchemes _auth;

    internal TransferModel(RawClient rawClient, Server server, AuthSchemes auth)
    {
        _rawClient = rawClient;
        _server = server;
        _auth = auth;
    }

    /// <summary>
    /// &lt;div class="msgbox_important"&gt;&lt;p class="msgbox_importantInDiv" data-mc-autonum="&amp;lt;b&amp;gt;&amp;lt;span style=&amp;quot;color: #dd1e31;&amp;quot; class=&amp;quot;mcFormatColor&amp;quot;&amp;gt;Important! &amp;lt;/span&amp;gt;&amp;lt;/b&amp;gt;"&gt;&lt;span class="autonumber"&gt;&lt;span&gt;&lt;b&gt;&lt;span style="color: #dd1e31;" class="mcFormatColor"&gt;Important!&lt;/span&gt;&lt;/b&gt;&lt;/span&gt;&lt;/span&gt; Due to EU &amp;amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all &lt;b&gt;Finances API&lt;/b&gt; methods. Please refer to &lt;a href="/develop/guides/digital-signatures-for-apis " target="_blank"&gt;Digital Signatures for APIs&lt;/a&gt; to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.&lt;/p&gt;&lt;/div&gt;&lt;br&gt;This method retrieves detailed information regarding a &lt;code&gt;TRANSFER&lt;/code&gt; transaction type. A &lt;code&gt;TRANSFER&lt;/code&gt; is a  monetary transaction type that involves a seller transferring money to eBay for reimbursement of one or more charges. For example, when a seller reimburses eBay for a buyer refund.&lt;br&gt;&lt;br&gt;If an ID is passed into the URI that is an identifier for another transaction type, this call will return an http status code of &lt;code&gt;404 Not found&lt;/code&gt;.
    /// </summary>
    /// <param name="transferId">The unique identifier of the &lt;code&gt;TRANSFER&lt;/code&gt; transaction type you wish to retrieve.</param>
    /// <param name="xEbayCMarketplaceId">This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See &lt;a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank "&gt;HTTP request headers&lt;/a&gt; for the marketplace ID values.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="Transfer"/> instance.</returns>
    /// <exception cref="SdkException{TResult}"> of <see cref="GetTransferError"/> when the server returns an error response.</exception>
    public Task<Transfer> GetTransfer(string transferId,
        string? xEbayCMarketplaceId,
        CancellationToken ct = default) =>
        _rawClient.Execute(_server.Default("/transfer/{transfer_Id}"),
            [new TemplateParam("transfer_Id", transferId)],
            [],
            [new HeaderParam("X-EBAY-C-MARKETPLACE-ID", xEbayCMarketplaceId)],
            HttpMethod.Get,
            EmptyBody.Instance,
            JsonResponse.Create<Transfer>(),
            GetTransferErrorResponse.Instance,
            [_auth.ApiAuth],
            ct);
}
