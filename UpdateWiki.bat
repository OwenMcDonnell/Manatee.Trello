cd Wiki
git pull
cd ..

git clone https://github.com/gregsdennis/AutoWiki.git
cd AutoWiki\AutoWiki

dotnet run -p AutoWiki.csproj ..\..\Wiki

cd ..\..\Wiki
git add -A
git commit -m "updated after build"
git push

cd ..