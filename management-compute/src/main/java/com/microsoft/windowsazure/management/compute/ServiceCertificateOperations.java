/**
 * 
 * Copyright (c) Microsoft and contributors.  All rights reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * 
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

package com.microsoft.windowsazure.management.compute;

import com.microsoft.windowsazure.core.OperationResponse;
import com.microsoft.windowsazure.core.OperationStatusResponse;
import com.microsoft.windowsazure.exception.ServiceException;
import com.microsoft.windowsazure.management.compute.models.ServiceCertificateCreateParameters;
import com.microsoft.windowsazure.management.compute.models.ServiceCertificateDeleteParameters;
import com.microsoft.windowsazure.management.compute.models.ServiceCertificateGetParameters;
import com.microsoft.windowsazure.management.compute.models.ServiceCertificateGetResponse;
import com.microsoft.windowsazure.management.compute.models.ServiceCertificateListResponse;
import java.io.IOException;
import java.net.URISyntaxException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import org.xml.sax.SAXException;

/**
* Operations for managing service certificates for your subscription.  (see
* http://msdn.microsoft.com/en-us/library/windowsazure/ee795178.aspx for more
* information)
*/
public interface ServiceCertificateOperations {
    /**
    * The Begin Creating Service Certificate operation adds a certificate to a
    * hosted service. This operation is an asynchronous operation. To
    * determine whether the management service has finished processing the
    * request, call Get Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460817.aspx for
    * more information)
    *
    * @param serviceName Required. The DNS prefix name of your service.
    * @param parameters Required. Parameters supplied to the Begin Creating
    * Service Certificate operation.
    * @throws ParserConfigurationException Thrown if there was an error
    * configuring the parser for the response body.
    * @throws SAXException Thrown if there was an error parsing the response
    * body.
    * @throws TransformerException Thrown if there was an error creating the
    * DOM transformer.
    * @throws IOException Signals that an I/O exception of some sort has
    * occurred. This class is the general class of exceptions produced by
    * failed or interrupted I/O operations.
    * @throws ServiceException Thrown if an unexpected response is found.
    * @return A standard service response including an HTTP status code and
    * request ID.
    */
    OperationResponse beginCreating(String serviceName, ServiceCertificateCreateParameters parameters) throws ParserConfigurationException, SAXException, TransformerException, IOException, ServiceException;
    
    /**
    * The Begin Creating Service Certificate operation adds a certificate to a
    * hosted service. This operation is an asynchronous operation. To
    * determine whether the management service has finished processing the
    * request, call Get Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460817.aspx for
    * more information)
    *
    * @param serviceName Required. The DNS prefix name of your service.
    * @param parameters Required. Parameters supplied to the Begin Creating
    * Service Certificate operation.
    * @return A standard service response including an HTTP status code and
    * request ID.
    */
    Future<OperationResponse> beginCreatingAsync(String serviceName, ServiceCertificateCreateParameters parameters);
    
    /**
    * The Begin Deleting Service Certificate operation deletes a service
    * certificate from the certificate store of a hosted service. This
    * operation is an asynchronous operation. To determine whether the
    * management service has finished processing the request, call Get
    * Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460803.aspx for
    * more information)
    *
    * @param parameters Required. Parameters supplied to the Begin Deleting
    * Service Certificate operation.
    * @throws IOException Signals that an I/O exception of some sort has
    * occurred. This class is the general class of exceptions produced by
    * failed or interrupted I/O operations.
    * @throws ServiceException Thrown if an unexpected response is found.
    * @return A standard service response including an HTTP status code and
    * request ID.
    */
    OperationResponse beginDeleting(ServiceCertificateDeleteParameters parameters) throws IOException, ServiceException;
    
    /**
    * The Begin Deleting Service Certificate operation deletes a service
    * certificate from the certificate store of a hosted service. This
    * operation is an asynchronous operation. To determine whether the
    * management service has finished processing the request, call Get
    * Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460803.aspx for
    * more information)
    *
    * @param parameters Required. Parameters supplied to the Begin Deleting
    * Service Certificate operation.
    * @return A standard service response including an HTTP status code and
    * request ID.
    */
    Future<OperationResponse> beginDeletingAsync(ServiceCertificateDeleteParameters parameters);
    
    /**
    * The Create Service Certificate operation adds a certificate to a hosted
    * service. This operation is an asynchronous operation. To determine
    * whether the management service has finished processing the request, call
    * Get Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460817.aspx for
    * more information)
    *
    * @param serviceName Required. The DNS prefix name of your service.
    * @param parameters Required. Parameters supplied to the Create Service
    * Certificate operation.
    * @throws InterruptedException Thrown when a thread is waiting, sleeping,
    * or otherwise occupied, and the thread is interrupted, either before or
    * during the activity. Occasionally a method may wish to test whether the
    * current thread has been interrupted, and if so, to immediately throw
    * this exception. The following code can be used to achieve this effect:
    * @throws ExecutionException Thrown when attempting to retrieve the result
    * of a task that aborted by throwing an exception. This exception can be
    * inspected using the Throwable.getCause() method.
    * @throws ServiceException Thrown if the server returned an error for the
    * request.
    * @throws ParserConfigurationException Thrown if there was an error
    * configuring the parser for the response body.
    * @throws SAXException Thrown if there was an error parsing the response
    * body.
    * @throws TransformerException Thrown if there was an error creating the
    * DOM transformer.
    * @throws IOException Signals that an I/O exception of some sort has
    * occurred. This class is the general class of exceptions produced by
    * failed or interrupted I/O operations.
    * @throws ServiceException Thrown if an unexpected response is found.
    * @throws URISyntaxException Thrown if there was an error parsing a URI in
    * the response.
    * @return The response body contains the status of the specified
    * asynchronous operation, indicating whether it has succeeded, is
    * inprogress, or has failed. Note that this status is distinct from the
    * HTTP status code returned for the Get Operation Status operation itself.
    * If the asynchronous operation succeeded, the response body includes the
    * HTTP status code for the successful request. If the asynchronous
    * operation failed, the response body includes the HTTP status code for
    * the failed request and error information regarding the failure.
    */
    OperationStatusResponse create(String serviceName, ServiceCertificateCreateParameters parameters) throws InterruptedException, ExecutionException, ServiceException, ParserConfigurationException, SAXException, TransformerException, IOException, URISyntaxException;
    
    /**
    * The Create Service Certificate operation adds a certificate to a hosted
    * service. This operation is an asynchronous operation. To determine
    * whether the management service has finished processing the request, call
    * Get Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460817.aspx for
    * more information)
    *
    * @param serviceName Required. The DNS prefix name of your service.
    * @param parameters Required. Parameters supplied to the Create Service
    * Certificate operation.
    * @return The response body contains the status of the specified
    * asynchronous operation, indicating whether it has succeeded, is
    * inprogress, or has failed. Note that this status is distinct from the
    * HTTP status code returned for the Get Operation Status operation itself.
    * If the asynchronous operation succeeded, the response body includes the
    * HTTP status code for the successful request. If the asynchronous
    * operation failed, the response body includes the HTTP status code for
    * the failed request and error information regarding the failure.
    */
    Future<OperationStatusResponse> createAsync(String serviceName, ServiceCertificateCreateParameters parameters);
    
    /**
    * The Delete Service Certificate operation deletes a service certificate
    * from the certificate store of a hosted service. This operation is an
    * asynchronous operation. To determine whether the management service has
    * finished processing the request, call Get Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460803.aspx for
    * more information)
    *
    * @param parameters Required. Parameters supplied to the Delete Service
    * Certificate operation.
    * @throws IOException Signals that an I/O exception of some sort has
    * occurred. This class is the general class of exceptions produced by
    * failed or interrupted I/O operations.
    * @throws ServiceException Thrown if an unexpected response is found.
    * @throws InterruptedException Thrown when a thread is waiting, sleeping,
    * or otherwise occupied, and the thread is interrupted, either before or
    * during the activity. Occasionally a method may wish to test whether the
    * current thread has been interrupted, and if so, to immediately throw
    * this exception. The following code can be used to achieve this effect:
    * @throws ExecutionException Thrown when attempting to retrieve the result
    * of a task that aborted by throwing an exception. This exception can be
    * inspected using the Throwable.getCause() method.
    * @throws ServiceException Thrown if the server returned an error for the
    * request.
    * @return The response body contains the status of the specified
    * asynchronous operation, indicating whether it has succeeded, is
    * inprogress, or has failed. Note that this status is distinct from the
    * HTTP status code returned for the Get Operation Status operation itself.
    * If the asynchronous operation succeeded, the response body includes the
    * HTTP status code for the successful request. If the asynchronous
    * operation failed, the response body includes the HTTP status code for
    * the failed request and error information regarding the failure.
    */
    OperationStatusResponse delete(ServiceCertificateDeleteParameters parameters) throws IOException, ServiceException, InterruptedException, ExecutionException;
    
    /**
    * The Delete Service Certificate operation deletes a service certificate
    * from the certificate store of a hosted service. This operation is an
    * asynchronous operation. To determine whether the management service has
    * finished processing the request, call Get Operation Status.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460803.aspx for
    * more information)
    *
    * @param parameters Required. Parameters supplied to the Delete Service
    * Certificate operation.
    * @return The response body contains the status of the specified
    * asynchronous operation, indicating whether it has succeeded, is
    * inprogress, or has failed. Note that this status is distinct from the
    * HTTP status code returned for the Get Operation Status operation itself.
    * If the asynchronous operation succeeded, the response body includes the
    * HTTP status code for the successful request. If the asynchronous
    * operation failed, the response body includes the HTTP status code for
    * the failed request and error information regarding the failure.
    */
    Future<OperationStatusResponse> deleteAsync(ServiceCertificateDeleteParameters parameters);
    
    /**
    * The Get Service Certificate operation returns the public data for the
    * specified X.509 certificate associated with a hosted service.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460792.aspx for
    * more information)
    *
    * @param parameters Required. Parameters supplied to the Get Service
    * Certificate operation.
    * @throws IOException Signals that an I/O exception of some sort has
    * occurred. This class is the general class of exceptions produced by
    * failed or interrupted I/O operations.
    * @throws ServiceException Thrown if an unexpected response is found.
    * @throws ParserConfigurationException Thrown if there was a serious
    * configuration error with the document parser.
    * @throws SAXException Thrown if there was an error parsing the XML
    * response.
    * @throws URISyntaxException Thrown if there was an error parsing a URI in
    * the response.
    * @return The Get Service Certificate operation response.
    */
    ServiceCertificateGetResponse get(ServiceCertificateGetParameters parameters) throws IOException, ServiceException, ParserConfigurationException, SAXException, URISyntaxException;
    
    /**
    * The Get Service Certificate operation returns the public data for the
    * specified X.509 certificate associated with a hosted service.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/ee460792.aspx for
    * more information)
    *
    * @param parameters Required. Parameters supplied to the Get Service
    * Certificate operation.
    * @return The Get Service Certificate operation response.
    */
    Future<ServiceCertificateGetResponse> getAsync(ServiceCertificateGetParameters parameters);
    
    /**
    * The List Service Certificates operation lists all of the service
    * certificates associated with the specified hosted service.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/jj154105.aspx for
    * more information)
    *
    * @param serviceName Required. The DNS prefix name of your hosted service.
    * @throws IOException Signals that an I/O exception of some sort has
    * occurred. This class is the general class of exceptions produced by
    * failed or interrupted I/O operations.
    * @throws ServiceException Thrown if an unexpected response is found.
    * @throws ParserConfigurationException Thrown if there was a serious
    * configuration error with the document parser.
    * @throws SAXException Thrown if there was an error parsing the XML
    * response.
    * @throws URISyntaxException Thrown if there was an error parsing a URI in
    * the response.
    * @return The List Service Certificates operation response.
    */
    ServiceCertificateListResponse list(String serviceName) throws IOException, ServiceException, ParserConfigurationException, SAXException, URISyntaxException;
    
    /**
    * The List Service Certificates operation lists all of the service
    * certificates associated with the specified hosted service.  (see
    * http://msdn.microsoft.com/en-us/library/windowsazure/jj154105.aspx for
    * more information)
    *
    * @param serviceName Required. The DNS prefix name of your hosted service.
    * @return The List Service Certificates operation response.
    */
    Future<ServiceCertificateListResponse> listAsync(String serviceName);
}