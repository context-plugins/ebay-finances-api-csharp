# Reference

> Source: [EBayFinancesApiClient](EBayFinancesApiClient.cs)

## PayoutModel

> Source: [PayoutModel](Api/PayoutModel.cs)

<details>
<summary><code>Task&lt;Payout&gt; GetPayout(string payoutId, string? xEbayCMarketplaceId, CancellationToken ct = default);</code></summary>

<dl>
<dd>

### Description

<dl>
<dd>

<div class="msgbox_important"><p class="msgbox_importantInDiv" data-mc-autonum="&lt;b&gt;&lt;span style=&quot;color: #dd1e31;&quot; class=&quot;mcFormatColor&quot;&gt;Important! &lt;/span&gt;&lt;/b&gt;"><span class="autonumber"><span><b><span style="color: #dd1e31;" class="mcFormatColor">Important!</span></b></span></span> Due to EU &amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all <b>Finances API</b> methods. Please refer to <a href="/develop/guides/digital-signatures-for-apis " target="_blank">Digital Signatures for APIs</a> to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.</p></div><br>This method retrieves details on a specific seller payout. The unique identfier of the payout is passed in as a path parameter at the end of the call URI. <br><br>The <b>getPayouts</b> method can be used to retrieve the unique identifier of a payout, or the user can check Seller Hub.

</dd>
</dl>

### Usage

<dl>
<dd>

```csharp
try
{
    var response = await client.PayoutModel.GetPayout(payoutId, xEbayCMarketplaceId);
    // TODO: Handle 'response' of type Payout
}
catch (SdkException<GetPayoutError> ex)
{
    if (ex.Error.TryGetError(out var error))
    {
        // TODO: Handle 'error' of type GetPayoutError
    }
}
```

</dd>
</dl>

### Parameters

<dl>
<dd>

| Name | Type | Description |
| --- | --- | --- |
| <code>payoutId</code> | <code>string</code> | The unique identfier of the payout is passed in as a path parameter at the end of the call URI. <br><br>The <b>getPayouts</b> method can be used to retrieve the unique identifier of a payout, or the user can check Seller Hub to get the payout ID. |
| <code>xEbayCMarketplaceId</code> | <code>string?</code> | This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See <a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank ">HTTP request headers</a> for the marketplace ID values. |

</dd>
</dl>

### Response

<dl>
<dd>

**OnSuccess**: <code>[Payout](Models/Payout.cs)</code>

**OnError**: <code>[SdkException](Core/Exceptions/SdkException.cs)&lt;[GetPayoutError](Errors/GetPayoutError.cs)&gt;</code>

</dd>
</dl>

</dd>
</dl>

</details>

<details>
<summary><code>Task&lt;PayoutSummaryResponse&gt; GetPayoutSummary(string? filter, string? xEbayCMarketplaceId, CancellationToken ct = default);</code></summary>

<dl>
<dd>

### Description

<dl>
<dd>

<div class="msgbox_important"><p class="msgbox_importantInDiv" data-mc-autonum="&lt;b&gt;&lt;span style=&quot;color: #dd1e31;&quot; class=&quot;mcFormatColor&quot;&gt;Important! &lt;/span&gt;&lt;/b&gt;"><span class="autonumber"><span><b><span style="color: #dd1e31;" class="mcFormatColor">Important!</span></b></span></span> Due to EU &amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all <b>Finances API</b> methods. Please refer to <a href="/develop/guides/digital-signatures-for-apis " target="_blank">Digital Signatures for APIs</a> to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.</p></div><br>This method is used to retrieve cumulative values for payouts in a particular state, or all states. The metadata in the response includes total payouts, the total number of monetary transactions (sales, refunds, credits) associated with those payouts, and the total dollar value of all payouts.<br><br>If the <b>filter</b> query parameter is used to filter by payout status, only one payout status value may be used. If the <b>filter</b> query parameter is not used to filter by a specific payout status, cumulative values for payouts in all states are returned.<br><br>The user can also use the <b>filter</b> query parameter to specify a date range, and then only payouts that were processed within that date range are considered.

</dd>
</dl>

### Usage

<dl>
<dd>

```csharp
try
{
    var response = await client.PayoutModel.GetPayoutSummary(filter, xEbayCMarketplaceId);
    // TODO: Handle 'response' of type PayoutSummaryResponse
}
catch (SdkException<GetPayoutSummaryError> ex)
{
    if (ex.Error.TryGetError(out var error))
    {
        // TODO: Handle 'error' of type GetPayoutSummaryError
    }
}
```

</dd>
</dl>

### Parameters

<dl>
<dd>

| Name | Type | Description |
| --- | --- | --- |
| <code>filter</code> | <code>string?</code> | The two filter types that can be used here are discussed below. One or both of these filter types can be used. If none of these filters are used, the data returned in the response will reflect payouts, in all states, processed within the last 90 days. <ul><li><b>payoutDate</b>: consider payouts processed within a specific range of dates. The date format to use is <code>YYYY-MM-DDTHH:MM:SS.SSSZ</code>. Below is the proper syntax to use if filtering by a date range: <br><br><code>https://apiz.ebay.com/sell/finances/v1/payout_summary?filter=payoutDate:[2018-12-17T00:00:01.000Z..2018-12-24T00:00:01.000Z]</code><br><br>Alternatively, the user could omit the ending date, and the date range would include the starting date and up to 90 days past that date, or the current date if the starting date is less than 90 days in the past.</li> <li><b>payoutStatus</b>: consider only the payouts in a particular state. Only one payout state can be specified with this filter. The supported <b>payoutStatus</b> values are as follows:<ul><li><code>INITIATED</code>: search for payouts that have been initiated but not processed.</li><li><code>SUCCEEDED</code>: consider only successful payouts.</li><li><code>RETRYABLE_FAILED</code>: consider only payouts that failed, but ones which will be tried again.</li><li><code>TERMINAL_FAILED</code>: consider only payouts that failed, and ones that will not be tried again.</li><li> <code>REVERSED</code>: consider only payouts that were reversed. </li></ul>Below is the proper syntax to use if filtering by payout status: <br><br><code>https://apiz.ebay.com/sell/finances/v1/payout_summary?filter=payoutStatus:{SUCCEEDED}</code></ul><br>If both the <b>payoutDate</b> and <b>payoutStatus</b> filters are used, only the payouts that satisfy both criteria are considered in the results. For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:FilterField |
| <code>xEbayCMarketplaceId</code> | <code>string?</code> | This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See <a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank ">HTTP request headers</a> for the marketplace ID values. |

</dd>
</dl>

### Response

<dl>
<dd>

**OnSuccess**: <code>[PayoutSummaryResponse](Models/PayoutSummaryResponse.cs)</code>

**OnError**: <code>[SdkException](Core/Exceptions/SdkException.cs)&lt;[GetPayoutSummaryError](Errors/GetPayoutSummaryError.cs)&gt;</code>

</dd>
</dl>

</dd>
</dl>

</details>

<details>
<summary><code>Task&lt;Payouts&gt; GetPayouts(string? filter, string? limit, string? offset, string? sort, string? xEbayCMarketplaceId, CancellationToken ct = default);</code></summary>

<dl>
<dd>

### Description

<dl>
<dd>

<div class="msgbox_important"><p class="msgbox_importantInDiv" data-mc-autonum="&lt;b&gt;&lt;span style=&quot;color: #dd1e31;&quot; class=&quot;mcFormatColor&quot;&gt;Important! &lt;/span&gt;&lt;/b&gt;"><span class="autonumber"><span><b><span style="color: #dd1e31;" class="mcFormatColor">Important!</span></b></span></span> Due to EU &amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all <b>Finances API</b> methods. Please refer to <a href="/develop/guides/digital-signatures-for-apis " target="_blank">Digital Signatures for APIs</a> to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.</p></div><br>This method is used to retrieve the details of one or more seller payouts. By using the <b>filter</b> query parameter, users can retrieve payouts processed within a specific date range, and/or they can retrieve payouts in a specific state.<br><br>There are also pagination and sort query parameters that allow users to control the payouts that are returned in the response.<br><br>If no payouts match the input criteria, an empty payload is returned.

</dd>
</dl>

### Usage

<dl>
<dd>

```csharp
try
{
    var response = await client.PayoutModel.GetPayouts(filter, limit, offset, sort, xEbayCMarketplaceId);
    // TODO: Handle 'response' of type Payouts
}
catch (SdkException<GetPayoutsError> ex)
{
    if (ex.Error.TryGetError(out var error))
    {
        // TODO: Handle 'error' of type GetPayoutsError
    }
}
```

</dd>
</dl>

### Parameters

<dl>
<dd>

| Name | Type | Description |
| --- | --- | --- |
| <code>filter</code> | <code>string?</code> | The three filter types that can be used here are discussed below. If none of these filters are used, all recent payouts in all states are returned:<ul><li><b>payoutDate</b>: search for payouts within a specific range of dates. The date format to use is <code>YYYY-MM-DDTHH:MM:SS.SSSZ</code>. Below is the proper syntax to use if filtering by a date range: <br><br><code>https://apiz.ebay.com/sell/finances/v1/payout?filter=payoutDate:[2018-12-17T00:00:01.000Z..2018-12-24T00:00:01.000Z]</code><br><br>Alternatively, the user could omit the ending date, and the date range would include the starting date and up to 90 days past that date, or the current date if the starting date is less than 90 days in the past.</li><li><b>lastAttemptedPayoutDate</b>: search for attempted payouts that failed within a specific range of dates. In order to use this filter, the <b>payoutStatus</b> filter must also be used and its value must be set to <code>RETRYABLE_FAILED</code>. The date format to use is <code>YYYY-MM-DDTHH:MM:SS.SSSZ</code>. The same syntax used for the <b>payoutDate</b> filter is also used for the <b>lastAttemptedPayoutDate</b> filter. <br><br>This filter is only applicable (and will return results) if one or more seller payouts have failed, but are retryable.</li> <li><b>payoutStatus</b>: search for payouts in a particular state. Only one payout state can be specified with this filter. The supported <b>payoutStatus</b> values are as follows:<ul><li><code>INITIATED</code>: search for payouts that have been initiated but not processed.</li><li><code>SUCCEEDED</code>: search for successful payouts.</li><li><code>RETRYABLE_FAILED</code>: search for payouts that failed, but ones which will be tried again. This value must be used if filtering by <b>lastAttemptedPayoutDate</b>. </li><li><code>TERMINAL_FAILED</code>: search for payouts that failed, and ones that will not be tried again.</li><li> <code>REVERSED</code>: search for payouts that were reversed. </li></ul>Below is the proper syntax to use if filtering by payout status: <br><br><code>https://apiz.ebay.com/sell/finances/v1/payout?filter=payoutStatus:{SUCCEEDED}</code></ul><br>If both the <b>payoutDate</b> and <b>payoutStatus</b> filters are used, payouts must satisfy both criteria to be returned. For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:FilterField |
| <code>limit</code> | <code>string?</code> | The number of payouts to return per page of the result set. Use this parameter in conjunction with the <b>offset</b> parameter to control the pagination of the output. <br><br> For example, if <b>offset</b> is set to <code>10</code> and <b>limit</b> is set to <code>10</code>, the method retrieves payouts 11 thru 20 from the result set. <br><br> <span class="tablenote"><strong>Note:</strong> This feature employs a zero-based list, where the first payout in the results set has an offset value of <code>0</code>. </span> <br><br> <b>Maximum:</b> <code>200</code> <br> <b>Default:</b> <code>20</code> |
| <code>offset</code> | <code>string?</code> | This integer value indicates the actual position that the first payout returned on the current page has in the results set. So, if you wanted to view the 11th payout of the result set, you would set the <strong>offset</strong> value in the request to <code>10</code>. <br><br>In the request, you can use the <b>offset</b> parameter in conjunction with the <b>limit</b> parameter to control the pagination of the output. For example, if <b>offset</b> is set to <code>30</code> and <b>limit</b> is set to <code>10</code>, the method retrieves payouts 31 thru 40 from the resulting collection of payouts. <br><br> <span class="tablenote"><strong>Note:</strong> This feature employs a zero-based list, where the first payout in the results set has an offset value of <code>0</code>.</span><br><br><b>Default:</b> <code>0</code> (zero) |
| <code>sort</code> | <code>string?</code> | By default, payouts or failed payouts that match the input criteria are sorted in descending order according to the payout date/last attempted payout date (i.e., most recent payouts returned first).<br><br>To view payouts in ascending order instead (i.e., oldest payouts/attempted payouts first,) you would include the <b>sort</b> query parameter, and then set the value of its <b>field</b> parameter to <code>payoutDate</code> or <code>lastAttemptedPayoutDate</code> (if searching for failed, retryable payouts). Below is the proper syntax to use if filtering by a payout date range in ascending order:<br><br><code>https://apiz.ebay.com/sell/finances/v1/payout?filter=payoutDate:[2018-12-17T00:00:01.000Z..2018-12-24T00:00:01.000Z]&sort=payoutDate</code><br><br>Payouts can only be sorted according to payout date, and can not be sorted by payout status. For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:SortField |
| <code>xEbayCMarketplaceId</code> | <code>string?</code> | This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See <a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank ">HTTP request headers</a> for the marketplace ID values. |

</dd>
</dl>

### Response

<dl>
<dd>

**OnSuccess**: <code>[Payouts](Models/Payouts.cs)</code>

**OnError**: <code>[SdkException](Core/Exceptions/SdkException.cs)&lt;[GetPayoutsError](Errors/GetPayoutsError.cs)&gt;</code>

</dd>
</dl>

</dd>
</dl>

</details>

## SellerFundsSummary

> Source: [SellerFundsSummary](Api/SellerFundsSummary.cs)

<details>
<summary><code>Task&lt;SellerFundsSummaryResponse&gt; GetSellerFundsSummary(string? xEbayCMarketplaceId, CancellationToken ct = default);</code></summary>

<dl>
<dd>

### Description

<dl>
<dd>

<div class="msgbox_important"><p class="msgbox_importantInDiv" data-mc-autonum="&lt;b&gt;&lt;span style=&quot;color: #dd1e31;&quot; class=&quot;mcFormatColor&quot;&gt;Important! &lt;/span&gt;&lt;/b&gt;"><span class="autonumber"><span><b><span style="color: #dd1e31;" class="mcFormatColor">Important!</span></b></span></span> Due to EU &amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all <b>Finances API</b> methods. Please refer to <a href="/develop/guides/digital-signatures-for-apis " target="_blank">Digital Signatures for APIs</a> to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.</p></div><br>This method retrieves all pending funds that have not yet been distibuted through a seller payout.<br><br>There are no input parameters for this method. The response payload includes available funds, funds being processed, funds on hold, and also an aggregate count of all three of these categories.<br><br>If there are no funds that are pending, on hold, or being processed for the seller's account, no response payload is returned, and an http status code of <code>204 - No Content</code> is returned instead.

</dd>
</dl>

### Usage

<dl>
<dd>

```csharp
try
{
    var response = await client.SellerFundsSummary.GetSellerFundsSummary(xEbayCMarketplaceId);
    // TODO: Handle 'response' of type SellerFundsSummaryResponse
}
catch (SdkException<GetSellerFundsSummaryError> ex)
{
    if (ex.Error.TryGetError(out var error))
    {
        // TODO: Handle 'error' of type GetSellerFundsSummaryError
    }
}
```

</dd>
</dl>

### Parameters

<dl>
<dd>

| Name | Type | Description |
| --- | --- | --- |
| <code>xEbayCMarketplaceId</code> | <code>string?</code> | This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See <a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank ">HTTP request headers</a> for the marketplace ID values. |

</dd>
</dl>

### Response

<dl>
<dd>

**OnSuccess**: <code>[SellerFundsSummaryResponse](Models/SellerFundsSummaryResponse.cs)</code>

**OnError**: <code>[SdkException](Core/Exceptions/SdkException.cs)&lt;[GetSellerFundsSummaryError](Errors/GetSellerFundsSummaryError.cs)&gt;</code>

</dd>
</dl>

</dd>
</dl>

</details>

## TransactionModel

> Source: [TransactionModel](Api/TransactionModel.cs)

<details>
<summary><code>Task&lt;TransactionSummaryResponse&gt; GetTransactionSummary(string? filter, string? xEbayCMarketplaceId, CancellationToken ct = default);</code></summary>

<dl>
<dd>

### Description

<dl>
<dd>

<div class="msgbox_important"><p class="msgbox_importantInDiv" data-mc-autonum="&lt;b&gt;&lt;span style=&quot;color: #dd1e31;&quot; class=&quot;mcFormatColor&quot;&gt;Important! &lt;/span&gt;&lt;/b&gt;"><span class="autonumber"><span><b><span style="color: #dd1e31;" class="mcFormatColor">Important!</span></b></span></span> Due to EU &amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all <b>Finances API</b> methods. Please refer to <a href="/develop/guides/digital-signatures-for-apis " target="_blank">Digital Signatures for APIs</a> to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.</p></div><br>The <b>getTransactionSummary</b> method retrieves cumulative information for monetary transactions. If applicable, the number of payments with a <code>transactionStatus</code> equal to <code>FUNDS_ON_HOLD</code> and the total monetary amount of these on-hold payments are also returned.<br><br><span class="tablenote"><b>Note:</b> For a complete list of transaction types, refer to <a href="/api-docs/sell/finances/types/pay:TransactionTypeEnum " target="_blank ">TransactionTypeEnum</a>.</span><br>Refer to the <a href="#uri.filter ">filter</a> field for additional information about each filter and its use.<br><br><span class="tablenote"><b>Note:</b> Unless a <code>transactionType</code> filter is used to retrieve a specific type of transaction (e.g., <code>SALE</code>, <code>REFUND</code>, etc.,) the <a href="#response.creditCount">creditCount</a> and <a href="#response.creditAmount">creditAmount</a> response fields both include <i>order sales</i> and <i>seller credits</i> information. That is, the <b>count</b> and <b>value</b> fields do not distinguish between these two types monetary transactions.</span>

</dd>
</dl>

### Usage

<dl>
<dd>

```csharp
try
{
    var response = await client.TransactionModel.GetTransactionSummary(filter, xEbayCMarketplaceId);
    // TODO: Handle 'response' of type TransactionSummaryResponse
}
catch (SdkException<GetTransactionSummaryError> ex)
{
    if (ex.Error.TryGetError(out var error))
    {
        // TODO: Handle 'error' of type GetTransactionSummaryError
    }
}
```

</dd>
</dl>

### Parameters

<dl>
<dd>

| Name | Type | Description |
| --- | --- | --- |
| <code>filter</code> | <code>string?</code> | Numerous filters are available for the <strong>getTransactionSummary</strong> method, and these filters are discussed below. One or more of these filter types can be used. The <b>transactionStatus</b> filter must be used. All other filters are optional. <ul><li><b>transactionStatus</b>: the data returned in the response pertains to the sales, payouts, and transfer status set. The supported <b>transactionStatus</b> values are as follows:<ul><li><code>PAYOUT</code>: only consider monetary transactions where the proceeds from the sales order(s) have been paid out to the seller's bank account.</li><li><code>FUNDS_PROCESSING</code>: only consider monetary transactions where the proceeds from the sales order(s) are currently being processed.</li><li><code>FUNDS_AVAILABLE_FOR_PAYOUT</code>: only consider monetary transactions where the proceeds from the sales order(s) are available for a seller payout, but processing has not yet begun.</li><li><code>FUNDS_ON_HOLD</code>: only consider monetary transactions where the proceeds from the sales order(s) are currently being held by eBay, and are not yet available for a seller payout.</li><li><code>COMPLETED</code>: this indicates that the funds for the corresponding <code>TRANSFER</code> monetary transaction have transferred and the transaction has completed.</li><li><code>FAILED</code>: this indicates the process has failed for the corresponding <code>TRANSFER</code> monetary transaction. </li></ul>Below is the proper syntax to use when setting up the <b>transactionStatus</b> filter: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=transactionStatus:&#123;PAYOUT&#125;</code></li><li><b>transactionDate</b>: only consider monetary transactions that occurred within a specific range of dates.<br><br><span class="tablenote"><strong>Note:</strong> All dates must be input using UTC format (<code>YYYY-MM-DDTHH:MM:SS.SSSZ</code>) and should be adjusted accordingly for the local timezone of the user.</span><br><br>Below is the proper syntax to use if filtering by a date range: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=transactionDate:&#91;2018-10-23T00:00:01.000Z..2018-11-09T00:00:01.000Z&#93;</code><br><br>Alternatively, the user could omit the ending date, and the date range would include the starting date and up to 90 days past that date, or the current date if the starting date is less than 90 days in the past.</li>  <li><b>transactionType</b>: only consider a specific type of monetary transaction. The supported <b>transactionType</b> values are as follows:<ul><li><code>SALE</code>: a sales order.</li><li> <code>REFUND</code>: a refund to the buyer after an order cancellation or return.</li><li><code>CREDIT</code>: a credit issued by eBay to the seller's account.</li><li><code>DISPUTE</code>: a monetary transaction associated with a payment dispute between buyer and seller.</li><li><code>NON_SALE_CHARGE</code>: a monetary transaction involving a seller transferring money to eBay for the balance of a charge for NON_SALE_CHARGE transactions (transactions that contain non-transactional seller fees). These can include a one-time payment, monthly/yearly subscription fees charged monthly, NRC charges, and fee credits.</li><li><code>SHIPPING_LABEL</code>: a monetary transaction where eBay is billing the seller for an eBay shipping label. Note that the shipping label functionality will initially only be available to a select number of sellers.</li><li><code>TRANSFER</code>: A transfer is a monetary transaction where eBay is billing the seller for reimbursement of a charge. An example of a transfer is a seller reimbursing eBay for a buyer refund.</li><li><code>ADJUSTMENT</code>: An adjustment is a monetary transaction where eBay is crediting or debiting the seller's account.</li><li><code>WITHDRAWAL</code>: A withdrawal is a monetary transaction where the seller requests an on-demand payout from eBay.<br><br><span class="tablenote"><b>Note:</b> On-demand payout is not available for sellers who are already on a <b>daily</b> payout schedule. In order to initiate an on-demand payout, a seller must be on a <b>weekly</b>, <b>bi-weekly</b>, or <b>monthly</b> payout schedule.</span></li><li><code>LOAN_REPAYMENT</code>: A monetary transaction related to the repayment of an outstanding loan balance for approved participants enrolled in the <a href="https://pages.ebay.com/sellercapital/ " target="_blank ">eBay Seller Capital financing program</a>.<br><br><span class="tablenote"><b>Note:</b> eBay Seller Capital financing is only available in select marketplaces. Refer to <a href="/api-docs/sell/finances/overview.html#loans " target="_blank ">Marketplace availability for eBay Seller Capital funding program</a> for current marketplace eligibility.</span></li></ul>Below is the proper syntax to use if filtering by a monetary transaction type: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=transactionType:{SALE}</code></li><li><b>buyerUsername</b>: only consider monetary transactions involving a specific buyer (specified with the buyer's eBay user ID). Below is the proper syntax to use if filtering by a specific eBay buyer: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=buyerUsername:&#123;buyer1234&#125;</code> </li><li><b>salesRecordReference</b>: only consider monetary transactions corresponding to a specific order (identified with a Selling Manager order identifier). Below is the proper syntax to use if filtering by a specific Selling Manager Sales Record ID: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=salesRecordReference:&#123;123&#125;</code><br><br><span class="tablenote"><strong>Note:</strong> For all orders originating after February 1, 2020, a value of <code>0</code> will be returned in the <b>salesRecordReference</b> field. So, this filter will only be useful to retrieve orders than occurred before this date.</span> </li><li><b>payoutId</b>: only consider monetary transactions related to a specific seller payout (identified with a Payout ID). This value is auto-generated by eBay once the seller payout is set to be processed. Below is the proper syntax to use if filtering by a specific Payout ID: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=payoutId:&#123;5********8&#125;</code>  </li><li><b>transactionId</b>: the unique identifier of a monetary transaction. For a sales order, the <b>orderId</b> filter should be used instead. Only the monetary transaction(s) associated with this <b>transactionId</b> value are returned.<br><br><span class="tablenote"><strong>Note:</strong> This filter cannot be used alone; the <b>transactionType</b> must also be specified when filtering by transaction ID.</span><br><br>Below is the proper syntax to use if filtering by a specific transaction ID: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=transactionId:{0*-0***0-3***3}&filter=transactionType:{SALE}</code> </li><li><b>orderId</b>: the unique identifier of a sales order. For any other monetary transaction, the <b>transactionId</b> filter should be used instead. Only the monetary transaction(s) associated with this <b>orderId</b> value are returned. Below is the proper syntax to use if filtering by a specific order ID: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction_summary?filter=orderId:{0*-0***0-3***3}</code> </li></ul> For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:FilterField |
| <code>xEbayCMarketplaceId</code> | <code>string?</code> | This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See <a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank ">HTTP request headers</a> for the marketplace ID values. |

</dd>
</dl>

### Response

<dl>
<dd>

**OnSuccess**: <code>[TransactionSummaryResponse](Models/TransactionSummaryResponse.cs)</code>

**OnError**: <code>[SdkException](Core/Exceptions/SdkException.cs)&lt;[GetTransactionSummaryError](Errors/GetTransactionSummaryError.cs)&gt;</code>

</dd>
</dl>

</dd>
</dl>

</details>

<details>
<summary><code>Task&lt;Transactions&gt; GetTransactions(string? filter, string? limit, string? offset, string? sort, string? xEbayCMarketplaceId, CancellationToken ct = default);</code></summary>

<dl>
<dd>

### Description

<dl>
<dd>

<div class="msgbox_important"><p class="msgbox_importantInDiv" data-mc-autonum="&lt;b&gt;&lt;span style=&quot;color: #dd1e31;&quot; class=&quot;mcFormatColor&quot;&gt;Important! &lt;/span&gt;&lt;/b&gt;"><span class="autonumber"><span><b><span style="color: #dd1e31;" class="mcFormatColor">Important!</span></b></span></span> Due to EU &amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all <b>Finances API</b> methods. Please refer to <a href="/develop/guides/digital-signatures-for-apis " target="_blank">Digital Signatures for APIs</a> to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.</p></div><br>The <b>getTransactions</b> method allows a seller to retrieve information about one or more of their monetary transactions.<br><br><span class="tablenote"><b>Note:</b> For a complete list of transaction types, refer to <a href="/api-docs/sell/finances/types/pay:TransactionTypeEnum " target="_blank ">TransactionTypeEnum</a>.</span><br>Numerous input filters are available which can be used individualy or combined to refine the data that are returned. For example:<ul><li><code>SALE</code> transactions for August 15, 2022;</li><li><code>RETURN</code> transactions for the month of January, 2021;</li><li>Transactions currently in a <code>transactionStatus</code> equal to <code>FUNDS_ON_HOLD</code>.</li></ul>Refer to the <a href="#uri.filter ">filter</a> field for additional information about each filter and its use.<br><br>Pagination and sort query parameters are also provided that allow users to further control how monetary transactions are displayed in the response.<br><br>If no monetary transactions match the input criteria, an http status code of <em>204 No Content</em> is returned with no response payload.

</dd>
</dl>

### Usage

<dl>
<dd>

```csharp
try
{
    var response = await client.TransactionModel.GetTransactions(filter, limit, offset, sort, xEbayCMarketplaceId);
    // TODO: Handle 'response' of type Transactions
}
catch (SdkException<GetTransactionsError> ex)
{
    if (ex.Error.TryGetError(out var error))
    {
        // TODO: Handle 'error' of type GetTransactionsError
    }
}
```

</dd>
</dl>

### Parameters

<dl>
<dd>

| Name | Type | Description |
| --- | --- | --- |
| <code>filter</code> | <code>string?</code> | Numerous filters are available for the <strong>getTransactions</strong> method, and these filters are discussed below. One or more of these filter types can be used. If none of these filters are used, all monetary transactions from the last 90 days are returned:<ul><li><b>transactionDate</b>: search for monetary transactions that occurred within a specific range of dates.<br><br><span class="tablenote"><strong>Note:</strong> All dates must be input using UTC format (<code>YYYY-MM-DDTHH:MM:SS.SSSZ</code>) and should be adjusted accordingly for the local timezone of the user.</span><br><br>Below is the proper syntax to use if filtering by a date range: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=transactionDate:[2018-10-23T00:00:01.000Z..2018-11-09T00:00:01.000Z]</code><br><br>Alternatively, the user could omit the ending date, and the date range would include the starting date and up to 90 days past that date, or the current date if the starting date is less than 90 days in the past.</li>  <li><b>transactionType</b>: search for a specific type of monetary transaction. The supported <b>transactionType</b> values are as follows:<ul><li><code>SALE</code>: a sales order.</li><li> <code>REFUND</code>: a refund to the buyer after an order cancellation or return.</li><li><code>CREDIT</code>: a credit issued by eBay to the seller's account.</li><li><code>DISPUTE</code>: a monetary transaction associated with a payment dispute between buyer and seller.</li><li><code>NON_SALE_CHARGE</code>: a monetary transaction involving a seller transferring money to eBay for the balance of a charge for NON_SALE_CHARGE transactions (transactions that contain non-transactional seller fees). These can include a one-time payment, monthly/yearly subscription fees charged monthly, NRC charges, and fee credits.</li><li><code>SHIPPING_LABEL</code>: a monetary transaction where eBay is billing the seller for an eBay shipping label. Note that the shipping label functionality will initially only be available to a select number of sellers.</li><li><code>TRANSFER</code>: A transfer is a monetary transaction where eBay is billing the seller for reimbursement of a charge. An example of a transfer is a seller reimbursing eBay for a buyer refund.</li><li><code>ADJUSTMENT</code>: An adjustment is a monetary transaction where eBay is crediting or debiting the seller's account.</li><li><code>WITHDRAWAL</code>: A withdrawal is a monetary transaction where the seller requests an on-demand payout from eBay.<br><br><span class="tablenote"><b>Note:</b> On-demand payout is not available for sellers who are already on a <b>daily</b> payout schedule. In order to initiate an on-demand payout, a seller must be on a <b>weekly</b>, <b>bi-weekly</b>, or <b>monthly</b> payout schedule.</span></li><li><code>LOAN_REPAYMENT</code>: A monetary transaction related to the repayment of an outstanding loan balance for approved participants enrolled in the <a href="https://pages.ebay.com/sellercapital/ " target="_blank ">eBay Seller Capital financing program</a>.<br><br><span class="tablenote"><b>Note:</b> eBay Seller Capital financing is only available in select marketplaces. Refer to <a href="/api-docs/sell/finances/overview.html#loans " target="_blank ">Marketplace availability for eBay Seller Capital funding program</a> for current marketplace eligibility.</span></li></ul>Below is the proper syntax to use if filtering by a monetary transaction type: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=transactionType:{SALE}</code></li><li><b>transactionStatus</b>: this filter type is only applicable for sales orders, and allows the user to filter seller payouts in a particular state.  The supported <b>transactionStatus</b> values are as follows:<ul><li><code>PAYOUT</code>: this indicates that the proceeds from the corresponding sales order has been paid out to the seller's account.</li><li><code>FUNDS_PROCESSING</code>: this indicates that the funds for the corresponding monetary transaction are currently being processed.</li><li><code>FUNDS_AVAILABLE_FOR_PAYOUT</code>: this indicates that the proceeds from the corresponding sales order are available for a seller payout, but processing has not yet begun.</li><li><code>FUNDS_ON_HOLD</code>: this indicates that the proceeds from the corresponding sales order are currently being held by eBay, and are not yet available for a seller payout.</li><li><code>COMPLETED</code>: this indicates that the funds for the corresponding <code>TRANSFER</code> monetary transaction have transferred and the transaction has completed.</li><li><code>FAILED</code>: this indicates the process has failed for the corresponding <code>TRANSFER</code> monetary transaction. </li></ul>Below is the proper syntax to use if filtering by transaction status: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=transactionStatus:{PAYOUT}</code></li><li><b>buyerUsername</b>: the eBay user ID of the buyer involved in the monetary transaction. Only monetary transactions involving this buyer are returned. Below is the proper syntax to use if filtering by a specific eBay buyer: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=buyerUsername:{buyer1234}</code> </li><li><b>salesRecordReference</b>: the unique Selling Manager identifier of the order involved in the monetary transaction. Only monetary transactions involving this Selling Manager Sales Record ID are returned. Below is the proper syntax to use if filtering by a specific Selling Manager Sales Record ID: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=salesRecordReference:{123}</code><br><br><span class="tablenote"><strong>Note:</strong> For all orders originating after February 1, 2020, a value of <code>0</code> will be returned in the <b>salesRecordReference</b> field. So, this filter will only be useful to retrieve orders than occurred before this date. </span></li><li><b>payoutId</b>: the unique identifier of a seller payout. This value is auto-generated by eBay once the seller payout is set to be processed. Only monetary transactions involving this Payout ID are returned. Below is the proper syntax to use if filtering by a specific Payout ID: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=payoutId:{5********8}</code>  </li><li><b>transactionId</b>: the unique identifier of a monetary transaction. For a sales order, the <b>orderId</b> filter should be used instead. Only the monetary transaction defined by the identifier is returned.<br><br><span class="tablenote"><strong>Note:</strong> This filter cannot be used alone; the <b>transactionType</b> must also be specified when filtering by transaction ID.</span><br><br>Below is the proper syntax to use if filtering by a specific transaction ID: <br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=transactionId:{0*-0***0-3***3}&filter=transactionType:{SALE}</code> </li><li><b>orderId</b>: the unique identifier of a sales order. Only monetary transaction(s) associated with this <b>orderId</b> value are returned.<br><br>For any other monetary transaction, the <b>transactionId</b> filter should be used instead.<br><br>Below is the proper syntax to use if filtering by a specific order ID:<br><br><code>https://apiz.ebay.com/sell/finances/v1/transaction?filter=orderId:{0*-0***0-3***3}</code> </li></ul> For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:FilterField |
| <code>limit</code> | <code>string?</code> | The number of monetary transactions to return per page of the result set. Use this parameter in conjunction with the <b>offset</b> parameter to control the pagination of the output. <br><br> For example, if <b>offset</b> is set to <code>10</code> and <b>limit</b> is set to <code>10</code>, the method retrieves monetary transactions 11 thru 20 from the result set. <br><br> <span class="tablenote"><strong>Note:</strong> This feature employs a zero-based list, where the first item in the list has an offset of <code>0</code>.</span> <br><br> <b>Maximum:</b><code> 1000</code> <br> <b>Default:</b><code> 20</code> |
| <code>offset</code> | <code>string?</code> | This integer value indicates the actual position that the first monetary transaction returned on the current page has in the results set. So, if you wanted to view the 11th monetary transaction of the result set, you would set the <strong>offset</strong> value in the request to <code>10</code>. <br><br>In the request, you can use the <b>offset</b> parameter in conjunction with the <b>limit</b> parameter to control the pagination of the output. For example, if <b>offset</b> is set to <code>30</code> and <b>limit</b> is set to <code>10</code>, the method retrieves transactions 31 thru 40 from the resulting collection of transactions. <br><br> <span class="tablenote"><strong>Note:</strong> This feature employs a zero-based list, where the first item in the list has an offset of <code>0</code>.</span><br><b>Default:</b> <code>0</code> (zero) |
| <code>sort</code> | <code>string?</code> | Sorting is not yet available for the <b>getTransactions</b> method. By default, monetary transactions that match the input criteria are sorted in descending order according to the transaction date. For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:SortField |
| <code>xEbayCMarketplaceId</code> | <code>string?</code> | This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See <a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank ">HTTP request headers</a> for the marketplace ID values. |

</dd>
</dl>

### Response

<dl>
<dd>

**OnSuccess**: <code>[Transactions](Models/Transactions.cs)</code>

**OnError**: <code>[SdkException](Core/Exceptions/SdkException.cs)&lt;[GetTransactionsError](Errors/GetTransactionsError.cs)&gt;</code>

</dd>
</dl>

</dd>
</dl>

</details>

## TransferModel

> Source: [TransferModel](Api/TransferModel.cs)

<details>
<summary><code>Task&lt;Transfer&gt; GetTransfer(string transferId, string? xEbayCMarketplaceId, CancellationToken ct = default);</code></summary>

<dl>
<dd>

### Description

<dl>
<dd>

<div class="msgbox_important"><p class="msgbox_importantInDiv" data-mc-autonum="&lt;b&gt;&lt;span style=&quot;color: #dd1e31;&quot; class=&quot;mcFormatColor&quot;&gt;Important! &lt;/span&gt;&lt;/b&gt;"><span class="autonumber"><span><b><span style="color: #dd1e31;" class="mcFormatColor">Important!</span></b></span></span> Due to EU &amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all <b>Finances API</b> methods. Please refer to <a href="/develop/guides/digital-signatures-for-apis " target="_blank">Digital Signatures for APIs</a> to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.</p></div><br>This method retrieves detailed information regarding a <code>TRANSFER</code> transaction type. A <code>TRANSFER</code> is a  monetary transaction type that involves a seller transferring money to eBay for reimbursement of one or more charges. For example, when a seller reimburses eBay for a buyer refund.<br><br>If an ID is passed into the URI that is an identifier for another transaction type, this call will return an http status code of <code>404 Not found</code>.

</dd>
</dl>

### Usage

<dl>
<dd>

```csharp
try
{
    var response = await client.TransferModel.GetTransfer(transferId, xEbayCMarketplaceId);
    // TODO: Handle 'response' of type Transfer
}
catch (SdkException<GetTransferError> ex)
{
    if (ex.Error.TryGetError(out var error))
    {
        // TODO: Handle 'error' of type GetTransferError
    }
}
```

</dd>
</dl>

### Parameters

<dl>
<dd>

| Name | Type | Description |
| --- | --- | --- |
| <code>transferId</code> | <code>string</code> | The unique identifier of the <code>TRANSFER</code> transaction type you wish to retrieve. |
| <code>xEbayCMarketplaceId</code> | <code>string?</code> | This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See <a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank ">HTTP request headers</a> for the marketplace ID values. |

</dd>
</dl>

### Response

<dl>
<dd>

**OnSuccess**: <code>[Transfer](Models/Transfer.cs)</code>

**OnError**: <code>[SdkException](Core/Exceptions/SdkException.cs)&lt;[GetTransferError](Errors/GetTransferError.cs)&gt;</code>

</dd>
</dl>

</dd>
</dl>

</details>

