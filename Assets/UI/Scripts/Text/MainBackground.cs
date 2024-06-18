using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MainBackground : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public int textId = 0;

    private void Start()
    {
        textId = 0;
        StartCoroutine(this.WaitAndPrint());
    }

    public IEnumerator WaitAndPrint()
    {
        while (true)
        {
            string tmp = "";

            for (int i = textId; i < textId + 20; i++)
            {
                tmp += $"{this.StrBackground[i]}\n";
            }
                
            textId += 1;
            textMeshPro.text = tmp;

            if (textId + 20 > StrBackground.Length)
            {
                Debug.Log(textId);
                textId = 0;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // код для компиляции nginx + naxsi waf под Ubuntu в docker конейнере
    public string[] StrBackground =
    {
       "<color=#c586b6>FROM</color> <color=#3ac9a2>ubuntu</color>:latest",
       "<color=#c586b6>EXPOSE</color> 80",
       "<color=#c586b6>RUN</color> apt-get update && apt-get install -y \\",
       "    nano \\",
       "    build-essential \\",
       "    libpcre3-dev \\",
       "    libpcre3-dev \\    zlib1g \\",
       "    zlib1g-dev \\",
       "    libxml2-dev \\",
       "    libxslt1-dev \\",
       "    wget \\",
       "    git",
       "<color=#c586b6>RUN</color> mkdir /naxis_install && \\",
       "    cd /naxis_install && \\",
       "    git clone --recurse-submodules <color=#c586b6>https://github.com/nginx/nginx</color> && \\",
       "    git clone --recurse-submodules <color=#c586b6>https://github.com/wargio/naxsi.git</color> && \\",
       "    cd ./nginx && \\",
       "    ./auto/configure     --sbin-path=/usr/sbin/nginx \\",
       "                         --conf-path=/etc/nginx/nginx.conf \\",
       "                         --error-log-path=/var/log/nginx/error.log \\",
       "                         --http-log-path=/var/log/nginx/access.log \\",
       "                         --with-pcre \\",
       "                         --pid-path=/var/run/nginx.pid \\",
       "                         --with-http_ssl_module \\",
       "                         --add-dynamic-module=/naxis_install/naxsi/naxsi_src \\",
       "    && make \\",
       "    && make install \\",
       "    && cp /naxis_install/naxsi/naxsi_rules/naxsi_core.rules /etc/nginx/",
       "<color=#c586b6>RUN</color> rm /etc/nginx/nginx.conf && \\",
       "cat <<EOF > /etc/nginx/nginx.conf",
       "load_module /usr/local/nginx/modules/ngx_http_naxsi_module.so;",
       "worker_processes  1;",
       "events {",
       "    worker_connections  1024;",
       "}",
       "http {",
       "    include /etc/nginx/naxsi_core.rules;",
       "    include       mime.types;",
       "    default_type  application/octet-stream;",
       "    keepalive_timeout  65;",
       "    server {",
       "        listen 80;",
       "        server_name 127.0.0.1;",
       "        location / {",
       "                add_header Referrer-Policy same-origin;",
       "                add_header X-Frame-Options SAMEORIGIN;",
       "                add_header X-Content-Type-Options nosniff;",
       "                add_header X-XSS-Protection \"1; mode=block\";",
       "                add_header Content-Security-Policy \"default-src 'self' 'unsafe-inline' <color=#c586b6>http://cdnjs.cloudflare.com</color>; \";",
       "                proxy_pass <color=#c586b6>http://172.17.0.6:3000</color>;",
       "                proxy_http_version 1.1;",
       "                include /etc/nginx/my_rules.rules;",
       "                error_page 500 /etc/nginx/error_pages/500.html;",
       "                proxy_intercept_errors on;",
       "        }",
       "    }",
       "}",
       "EOF",
       "<color=#c586b6>COPY</color> my_rules.rules /etc/nginx/",
       "<color=#c586b6>COPY</color> naxsi_rules.rules /etc/nginx/",
       "<color=#c586b6>CMD</color> [\"nginx\", \"-g\", \"daemon off;\"]",
    };
}
