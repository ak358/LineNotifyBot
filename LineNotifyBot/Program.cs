using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{

    static async Task Main(string[] args)
    {
        try
        {
            string url = "https://notify-api.line.me/api/notify";
            string accessToken = "token";

            // フォームデータを作成
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("message", "123"),
                new KeyValuePair<string, string>("stickerPackageId", "446"),
                new KeyValuePair<string, string>("stickerId", "1988")
            });

            // HttpClientのインスタンスを作成
            using (var httpClient = new HttpClient(new DebugHandler()))
            {
                // Authorizationヘッダーを設定
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                // POSTリクエストを送信
                HttpResponseMessage response = await httpClient.PostAsync(url, formData);

                // レスポンスを表示
                Console.WriteLine("送信結果：" + response.StatusCode);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("エラーレスポンス：" + responseContent);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("送信エラー");
        }
    }
}

// HTTPリクエストとレスポンスをキャプチャするクラス
public class DebugHandler : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        Console.WriteLine("リクエスト:");
        Console.WriteLine(request.ToString());
        if (request.Content != null)
        {
            Console.WriteLine(await request.Content.ReadAsStringAsync());
        }
        Console.WriteLine();

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("レスポンス:");
        Console.WriteLine(response.ToString());
        if (response.Content != null)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
        Console.WriteLine();

        return response;
    }

    //JSONで投げない
    //    static async Task Main(string[] args)
    //    {
    //        try
    //        {
    //            string url = "https://notify-api.line.me/api/notify";
    //            string accessToken = "token";

    //            // JSONオブジェクトを作成
    //            var data = new
    //            {
    //                message = 123,
    //                stickerPackageId = 446,
    //                stickerId = 1988
    //            };

    //            // JsonSerializerOptionsを作成し、エンコーダーをカスタマイズする
    //            //var options = new JsonSerializerOptions
    //            //{
    //            //    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    //            //    WriteIndented = true // 必要に応じて改行とインデントを有効にする
    //            //};

    //            // JSONオブジェクトをJSON文字列に変換
    //            string json = JsonSerializer.Serialize(data);

    //            // 結果を表示
    //            Console.WriteLine(json);
    //            // JSONオブジェクトをJSON文字列に変換
    //            //string json = JsonSerializer.Serialize(data);

    //            // HttpClientのインスタンスを作成
    //            using (var httpClient = new HttpClient(new DebugHandler()))
    //            {
    //                // Authorizationヘッダーを設定
    //                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

    //                // JSONデータをStringContentに変換
    //                var content = new StringContent(json, Encoding.UTF8, "application/json");

    //                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

    //                // POSTリクエストを送信
    //                HttpResponseMessage response = await httpClient.PostAsync(url, content);

    //                // レスポンスを表示
    //                Console.WriteLine("送信結果：" + response.StatusCode);
    //                string responseContent = await response.Content.ReadAsStringAsync();
    //                Console.WriteLine("エラーレスポンス：" + responseContent);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            Console.WriteLine("送信エラー");
    //        }
    //    }
    //}

    //// HTTPリクエストとレスポンスをキャプチャするクラス
    //public class DebugHandler : HttpClientHandler
    //{
    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    //    {
    //        Console.WriteLine("リクエスト:");
    //        Console.WriteLine(request.ToString());
    //        if (request.Content != null)
    //        {
    //            Console.WriteLine(await request.Content.ReadAsStringAsync());
    //        }
    //        Console.WriteLine();

    //        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

    //        Console.WriteLine("レスポンス:");
    //        Console.WriteLine(response.ToString());
    //        if (response.Content != null)
    //        {
    //            Console.WriteLine(await response.Content.ReadAsStringAsync());
    //        }
    //        Console.WriteLine();

    //        return response;
    //    }
}
