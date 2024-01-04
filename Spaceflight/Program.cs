/* TODO - Per domani
 1. Rendere la casse file writer generica
 2. Spostare i metodi per la lettura di articoli e blog in una
    nuova classe SpaceFlightAPIClient
 3. Rendere i path relativi
 4. (Bonus) Creare degli overload asyncEnumerable dei metodi di SpaceFlightAPIClient
 */

/* TODO - Ulteriori modifiche
 1. Togliere la logica di paginazione dal metodo getArticlesAsync;
 2. Far si che il metodo getArticlesAsync accetti come parametro il
    numero di elementi da restuire;
 3. Utilizzare il baseAddress e utilizzare le costanti (Numero elementi per pagina);
 4. Fare lo stesso per blogs;
 5. Rivedere gli asyncEnumerable;
 6. (In aggiunta) Vedere differenze tra i metodi get di Assembly
 */

using System.Reflection;
using Spaceflight;

var projectDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location)!
    .Parent!.Parent!.Parent!.FullName;
var articlesFilePath = Path.Combine(projectDirectory, "Articles.txt");
var blogsFilePath = Path.Combine(projectDirectory, "Blogs.txt");

using var http = new HttpClient();
var spaceflightClient = new SpaceflightApiClient(http);

await using var articlesWriter = new FileWriter(articlesFilePath);
var articles = await spaceflightClient.GetArticlesAsync(100);
foreach (var article in articles)
{
    await articlesWriter.WriteAsync(article);
}

await using var blogsWriter = new FileWriter(blogsFilePath);
var blogs = await spaceflightClient.GetBlogsAsync(100);
foreach (var blog in blogs)
{
    await blogsWriter.WriteAsync(blog);
}