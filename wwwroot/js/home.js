// const PostCards = document.getElementById("post-cards");
// let CardContent = ``;

// for (let i = 0; i < 6; i++) {
//   CardContent += `
//           <div class="card w-full shadow-lg rounded-lg overflow-hidden p-3 bg-slate-50 hover:scale-[1.03] transition-transform duration-300">
//             <img src="images/Grand.jpg" alt="Post Image" class="w-full h-60 object-cover">
//             <div class="p-4">
//               <p class="card-category text-[#be9656] text-sm my-1 pt-5 tracking-widest text-center">
//                 ART / LIFESTYLE
//               </p>
//               <a href="/blogs/${i + 1}"><h2 class="card-title font-bold text-2xl text-center tracking-wider">Fashion Model Shoot</h2></a>
//               <p class="card-date text-center text-gray-400 my-2 text-sm italic">Posted On July 22, 2025</p>
//               <h5 class="card-context italic text-justify">
//                 Lorem ipsum dolor sit amet consectetur adipisicing elit. Dignissimos nisi ut quisquam cum non eius minus
//                 corporis, quo dicta assumenda repudiandae, modi maxime soluta aspernatur quaerat? ...
//               </h5>
//             </div>
//           </div>
//   `;
// }
// PostCards.innerHTML = CardContent;


const connection = new signalR.HubConnectionBuilder()
  .withUrl("/blogHub")
  .build();

connection.on("ReceiveNewBlog", function (blog) {
  const blogCards = document.querySelector("#blog-cards");

  const postedOn = new Date(blog.postedOn).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });

  const card = document.createElement("div");
  console.log(blog);
  card.innerHTML = `
        <div
          class="card w-full shadow-lg rounded-lg overflow-hidden p-3 bg-slate-50 hover:scale-[1.03] transition-transform duration-300">
          <img src="data:image/png;base64,${blog.image}" alt="Post Image" class="w-full h-60 object-cover">
          <div class="p-4">
            <p class="card-category text-[#be9656] text-sm my-1 pt-5 tracking-widest text-center">
              ${blog.category.toUpperCase()} / ${blog.type.toUpperCase()}
            </p>
            <a href="/blogs/${blog.blogId}">
              <h2 class="card-title font-bold text-2xl text-center tracking-wider">${blog.title} hello</h2>
            </a>
            <p class="card-date text-center text-gray-400 my-2 text-sm italic">Posted On ${postedOn}</p>
            <h5 class="card-context italic text-justify">
              ${blog.content.substring(0, 255)} ...
            </h5>
          </div>
        </div>
    `;
  blogCards.prepend(card);
});

connection.start().catch(err => console.error(err.toString()));