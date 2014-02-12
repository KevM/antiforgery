using HtmlTags;

namespace antiforgery
{
	public class TestEndpoint
	{
		public HtmlTag post_csrf(PostRequest request)
		{
			return new HtmlTag("h1", h => h.Text("POST HOLA"));
		}

		public HtmlTag get_csrf(GetRequest request)
		{
			return new HtmlTag("h1", h => h.Text("GET HOLA"));
		}
	}

	public class GetResult
	{
	}

	public class GetRequest
	{
	}

	public class PostRequest
	{
	}

	public class PostResult
	{
	}
}