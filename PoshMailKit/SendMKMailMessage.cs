﻿using System.Collections;
using System.Management.Automation;
using MimeKit;
using MimeKit.Text;
using PoshMailKit.Internals;
using MailKit;
using MailKit.Security;
using System.Net;

namespace PoshMailKit;

[Cmdlet(
    VerbsCommunications.Send, "MKMailMessage",
    DefaultParameterSetName = "Modern")]
public class SendMKMailMessage : PSCmdlet
{
    #region Cmdlet parameters

    #region Parameter Set: All

    #region Parameter: Attachments
    [Alias("PsPath")]
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true)]
    public string[] Attachments { get; set; }
    #endregion

    #region Parameter: Bcc
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public string[] Bcc { get; set; }
    #endregion

    #region Parameter: Body
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true,
        Position = 2)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true,
        Position = 2)]
    public string Body { get; set; }
    #endregion

    #region Parameter: Cc
    [Parameter
        (ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public string[] Cc { get; set; }
    #endregion

    #region Parameter: Credential
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public PSCredential Credential { get; set; }
    #endregion

    #region Parameter: From
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true,
        Mandatory = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true,
        Mandatory = true)]
    public string From { get; set; }
    #endregion

    #region Parameter: InlineAttachments
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public Hashtable InlineAttachments { get; set; }
    #endregion

    #region Parameter: Port
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public int Port { get; set; } = 25;
    #endregion

    #region Parameter: ReplyTo
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public string[] ReplyTo { get; set; }
    #endregion

    #region Parameter: SmtpServer
    [Alias("ComputerName")]
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true,
        Position = 3)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true,
        Position = 3)]
    public string SmtpServer { get; set; }
    #endregion

    #region Parameter: Subject
    [Alias("sub")]
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true,
        Position = 1)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true,
        Position = 1)]
    public string Subject { get; set; }
    #endregion

    #region Parameter: To
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true,
        Mandatory = true,
        Position = 0)]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true,
        Mandatory = true,
        Position = 0)]
    public string[] To { get; set; }
    #endregion

    #endregion

    #region Parameter Set: Modern

    #region Parameter: BodyFormat
    // Legacy counterpart: -BodyAsHtml
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    public TextFormat BodyFormat { get; set; } = TextFormat.Plain;
    #endregion

    #region Parameter: CharsetEncoding
    // Legacy counterpart: -Encoding (for both -CharsetEncoding and -ContentTransferEncoding)
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    public System.Text.Encoding CharsetEncoding { get; set; } = System.Text.Encoding.UTF8;
    #endregion

    #region Parameter: ContentTransferEncoding
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    public ContentEncoding ContentTransferEncoding { get; set; } = ContentEncoding.Base64;
    #endregion

    #region Parameter: DeliveryStatusNotification
    // Legacy counterpart: -DeliveryNotificationOptions
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    public DeliveryStatusNotification? DeliveryStatusNotification { get; set; }
    #endregion

    #region Parameter: MessagePriority
    // Legacy counterpart: -Priority
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    public MessagePriority MessagePriority { get; set; } = MessagePriority.Normal;
    #endregion

    #region Parameter: SecureSocketOptions
    // Legacy counterpart: -UseSsl
    [Parameter(
        ParameterSetName = "Modern",
        ValueFromPipelineByPropertyName = true)]
    public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.Auto;
    #endregion

    #endregion

    #region Parameter Set: Legacy

    #region Parameter: Legacy
    // Forces processing into Legacy mode
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public SwitchParameter Legacy { get; set; }
    #endregion

    #region Parameter: BodyAsHtml
    // Modern counterpart: -BodyFormat
    [Alias("BAH")]  
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public SwitchParameter BodyAsHtml { get; set; }
    #endregion

    #region Parameter: DeliveryNotificationOption
    // Modern counterpart: -DeliveryStatusNotification
    [Alias("DNO")]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public DeliveryNotificationOptions DeliveryNotificationOption { get; set; }
    #endregion

    #region Parameter: Encoding
    // Modern counterparts: -CharsetEncoding and -ContentTransferEncoding
    [Alias("BE")]
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public Encoding Encoding { get; set; }
    #endregion

    #region Parameter: Priority
    // Modern counterpart: -MessagePriority
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public MailPriority Priority { get; set; }
    #endregion

    #region Parameter: UseSsl
    // Modern counterpart: -SecureSocketOptions
    [Parameter(
        ParameterSetName = "Legacy",
        ValueFromPipelineByPropertyName = true)]
    public SwitchParameter UseSsl { get; set; }
    #endregion

    #endregion

    #endregion

    private MessageBuilder MailMessage { get; set; }
    private List<MimePart> FilesToAttach { get; set; }

    protected override void BeginProcessing()
    {
        ProcessParameters();
        ProcessAttachments();
    }

    protected override void ProcessRecord()
    {
        MailMessage = new MessageBuilder
        {
            Subject = Subject,
            Priority = MessagePriority,
            From = From,
            To = To ?? null,
            Cc = Cc ?? null,
            Bcc = Bcc ?? null,
            ReplyTo = ReplyTo ?? null,
        };

        MailMessage.NewMailBody(BodyFormat, CharsetEncoding, Body, ContentTransferEncoding);
        MailMessage.AddAttachments(FilesToAttach);

        SmtpProcessor processor = new()
        {
            SmtpServer = SmtpServer,
            SmtpPort = Port,
            SecureSocketOptions = SecureSocketOptions,
            Message = MailMessage.Message,
            Notification = DeliveryStatusNotification,
        };

        if (Credential != null)
            processor.Credential = (NetworkCredential)Credential;

        processor.SendMailMessage();
    }

    private void ProcessParameters()
    {
        if (string.IsNullOrEmpty(SmtpServer))
            SmtpServer = (string)SessionState.PSVariable.Get("PSEmailServer").Value;
        if (string.IsNullOrEmpty(SmtpServer))
        {
            string errorMessage = "The email cannot be sent because no SMTP server was specified. " +
                "You must specify an SMTP server by using either the SmtpServer parameter or the $PSEmailServer variable.";
            InvalidOperationException exception = new(errorMessage);
            ErrorRecord errorRecord = new(exception, "", ErrorCategory.InvalidArgument, null);
            ThrowTerminatingError(errorRecord);
        }

        if (ParameterSetName == "Legacy")
        {
            SetLegacySsl();
            SetLegacyPriority();
            SetLegacyEncoding();
            SetLegacyNotification();
            SetLegacyBodyFormat();
        }
    }

    private void ProcessAttachments()
    {
        string workingDirectory = SessionState.Path.CurrentFileSystemLocation.Path;
        FileProcessor fileProcessor = new(workingDirectory);

        FilesToAttach = new List<MimePart>();

        if (Attachments != null)
        {
            ContentDispositionType attachmentContent = ContentDispositionType.Attachment;
            foreach (string file in Attachments)
            {
                MimePart fileMimePart = fileProcessor.GetFileMimePart(file, attachmentContent);
                FilesToAttach.Add(fileMimePart);
            }
        }

        if (InlineAttachments != null)
        {
            ContentDispositionType inlineContent = ContentDispositionType.Inline;
            foreach (string label in InlineAttachments.Keys)
            {
                string file = (string)InlineAttachments[label];
                MimePart fileMimePart = fileProcessor.GetFileMimePart(file, inlineContent, label);
                FilesToAttach.Add(fileMimePart);
            }
        }
    }

    private void SetLegacySsl()
    {
        if (!UseSsl)
            SecureSocketOptions = SecureSocketOptions.None;
    }

    private void SetLegacyPriority()
    {
        // Translate priority; Enum default is Normal
        switch (Priority)
        {
            case MailPriority.Normal:
                MessagePriority = MessagePriority.Normal;
                break;
            case MailPriority.Low:
                MessagePriority = MessagePriority.NonUrgent;
                break;
            case MailPriority.High:
                MessagePriority = MessagePriority.Urgent;
                break;
        }
    }

    private void SetLegacyEncoding()
    {
        // Translate Encoding; Enum default is ASCII
        ContentTransferEncoding = ContentEncoding.QuotedPrintable;
        switch (Encoding)
        {
            case Encoding.ASCII:
                CharsetEncoding = System.Text.Encoding.ASCII;
                break;
            case Encoding.BigEndianUnicode:
                CharsetEncoding = System.Text.Encoding.BigEndianUnicode;
                ContentTransferEncoding = ContentEncoding.Base64;
                break;
            case Encoding.BigEndianUTF32:
                CharsetEncoding = System.Text.Encoding.GetEncoding("utf-32BE");
                break;
            case Encoding.Unicode:
                CharsetEncoding = System.Text.Encoding.Unicode;
                ContentTransferEncoding = ContentEncoding.Base64;
                break;
            case Encoding.UTF8:
                CharsetEncoding = System.Text.Encoding.UTF8;
                break;
            case Encoding.UTF8BOM:
                CharsetEncoding = System.Text.Encoding.UTF8;
                ContentTransferEncoding = ContentEncoding.Base64;
                break;
            case Encoding.UTF8NoBOM:
                CharsetEncoding = System.Text.Encoding.UTF8;
                break;
            case Encoding.UTF32:
                CharsetEncoding = System.Text.Encoding.UTF32;
                ContentTransferEncoding = ContentEncoding.Base64;
                break;
        }
    }

    private void SetLegacyNotification()
    {
        // Translate notification; default is null and does nothing
        switch (DeliveryNotificationOption)
        {
            case DeliveryNotificationOptions.OnSuccess:
                DeliveryStatusNotification = MailKit.DeliveryStatusNotification.Success;
                break;
            case DeliveryNotificationOptions.OnFailure:
                DeliveryStatusNotification = MailKit.DeliveryStatusNotification.Failure;
                break;
            case DeliveryNotificationOptions.Delay:
                DeliveryStatusNotification = MailKit.DeliveryStatusNotification.Delay;
                break;
            case DeliveryNotificationOptions.Never:
                DeliveryStatusNotification = MailKit.DeliveryStatusNotification.Never;
                break;
        }
    }

    private void SetLegacyBodyFormat()
    {
        if (BodyAsHtml)
            BodyFormat = TextFormat.Html;
    }
}
